using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Models;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class ProductListViewModel : ObservableObject
{
    private readonly ProductService _productService;

    [ObservableProperty]
    private List<Product> products = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string searchKeyword = string.Empty;

    [ObservableProperty]
    private decimal? minPrice;

    [ObservableProperty]
    private decimal? maxPrice;

    public ProductListViewModel()
    {
        _productService = new ProductService();
    }

    public void LoadProducts()
    {
        IsLoading = true;
        try
        {
            Products = _productService.GetAllProducts();
            OnPropertyChanged(nameof(Products));
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("Error loading products. Please try again.");
            System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
            Products = new List<Product>();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void NavigateToProductDetail(Product? product)
    {
        if (product != null)
        {
            var productDetailPageType = typeof(Uno_Platform.Views.ProductDetailPage);
            ServiceLocator.NavigationService.NavigateTo(productDetailPageType, product);
        }
    }

    [RelayCommand]
    private void RefreshProducts()
    {
        LoadProducts();
        ToastService.Instance.ShowSuccess("Products refreshed");
    }

    [RelayCommand]
    private void Search()
    {
        IsLoading = true;
        try
        {
            Products = _productService.SearchProducts(SearchKeyword);
            OnPropertyChanged(nameof(Products));
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("Error searching products.");
            System.Diagnostics.Debug.WriteLine($"Error searching: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void Filter()
    {
        IsLoading = true;
        try
        {
            Products = _productService.FilterByPrice(MinPrice, MaxPrice);
            OnPropertyChanged(nameof(Products));
            ToastService.Instance.ShowSuccess("Filter applied");
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("Error filtering products.");
            System.Diagnostics.Debug.WriteLine($"Error filtering: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void ClearFilter()
    {
        MinPrice = null;
        MaxPrice = null;
        SearchKeyword = string.Empty;
        LoadProducts();
    }
}

