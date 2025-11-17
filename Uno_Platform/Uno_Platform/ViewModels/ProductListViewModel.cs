using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using System.Collections.ObjectModel;
using Uno_Platform.Models;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class ProductListViewModel : ObservableObject
{
    private readonly ProductService _productService;
    private readonly CartService _cartService;
    private System.Threading.Timer? _searchDebounceTimer;

    ~ProductListViewModel()
    {
        // Cleanup timer on finalization
        _searchDebounceTimer?.Dispose();
    }

    [ObservableProperty]
    private List<Product> products = new();

    [ObservableProperty]
    private ObservableCollection<Product> filteredProducts = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string searchKeyword = string.Empty;

    [ObservableProperty]
    private decimal minPrice;

    [ObservableProperty]
    private decimal maxPrice = 10000;

    [ObservableProperty]
    private decimal currentMinPrice;

    [ObservableProperty]
    private decimal currentMaxPrice = 10000;

    public string CurrentMinPriceFormatted => CurrentMinPrice.ToString("F0");
    
    public string CurrentMaxPriceFormatted => CurrentMaxPrice.ToString("F0");

    [ObservableProperty]
    private string selectedCategory = "All";

    [ObservableProperty]
    private List<string> categories = new();

    [ObservableProperty]
    private int cartItemCount;

    public ProductListViewModel()
    {
        _productService = ServiceLocator.ProductService;
        _cartService = ServiceLocator.CartService;
        LoadData();
    }

    private async Task LoadDataAsync()
    {
        IsLoading = true;
        try
        {
            Products = await _productService.GetAllProductsAsync();
            Categories = await _productService.GetAllCategoriesAsync();
            Categories.Insert(0, "All");
            
            if (Products.Any())
            {
                MinPrice = Products.Min(p => p.Price);
                MaxPrice = Products.Max(p => p.Price);
                CurrentMinPrice = MinPrice;
                CurrentMaxPrice = MaxPrice;
            }
            
            ApplyFilters();
            await UpdateCartCount();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private void LoadData()
    {
        _ = LoadDataAsync();
    }

    [RelayCommand]
    private void Search()
    {
        // Debounce search
        _searchDebounceTimer?.Dispose();
        _searchDebounceTimer = new System.Threading.Timer(_ =>
        {
            DispatcherQueue.GetForCurrentThread()?.TryEnqueue(() =>
            {
                ApplyFilters();
            });
        }, null, 300, Timeout.Infinite);
    }

    partial void OnSearchKeywordChanged(string value)
    {
        Search();
    }

    partial void OnCurrentMinPriceChanged(decimal value)
    {
        OnPropertyChanged(nameof(CurrentMinPriceFormatted));
        ApplyFilters();
    }

    partial void OnCurrentMaxPriceChanged(decimal value)
    {
        OnPropertyChanged(nameof(CurrentMaxPriceFormatted));
        ApplyFilters();
    }

    partial void OnSelectedCategoryChanged(string value)
    {
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        IsLoading = true;
        try
        {
            var filtered = Products.AsEnumerable();

            // Search filter
            if (!string.IsNullOrWhiteSpace(SearchKeyword))
            {
                var keyword = SearchKeyword.ToLowerInvariant();
                filtered = filtered.Where(p =>
                    p.Name.ToLowerInvariant().Contains(keyword) ||
                    p.Category.ToLowerInvariant().Contains(keyword) ||
                    p.Description.ToLowerInvariant().Contains(keyword)
                );
            }

            // Category filter
            if (SelectedCategory != "All")
            {
                filtered = filtered.Where(p => p.Category == SelectedCategory);
            }

            // Price filter
            filtered = filtered.Where(p => p.Price >= CurrentMinPrice && p.Price <= CurrentMaxPrice);

            // Update ObservableCollection efficiently
            FilteredProducts.Clear();
            foreach (var product in filtered)
            {
                FilteredProducts.Add(product);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error applying filters: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddToCart(Product? product)
    {
        if (product == null) return;

        try
        {
            var success = await _cartService.AddToCartAsync(product.Id);
            if (success)
            {
                await UpdateCartCount();
                ToastService.Instance.ShowSuccess($"{product.Name} added to cart!");
            }
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("Failed to add to cart");
            System.Diagnostics.Debug.WriteLine($"Error adding to cart: {ex.Message}");
        }
    }

    [RelayCommand]
    private void NavigateToProductDetail(Product? product)
    {
        if (product != null)
        {
            var pageType = typeof(Uno_Platform.Views.ProductDetailPage);
            ServiceLocator.NavigationService.NavigateTo(pageType, product);
        }
    }

    [RelayCommand]
    private void NavigateToCart()
    {
        var pageType = typeof(Uno_Platform.Views.CartPage);
        ServiceLocator.NavigationService.NavigateTo(pageType);
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadDataAsync();
    }

    private async Task UpdateCartCount()
    {
        try
        {
            CartItemCount = await _cartService.GetCartItemCountAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating cart count: {ex.Message}");
        }
    }
}
