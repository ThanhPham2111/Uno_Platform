using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Models;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class ProductDetailViewModel : ObservableObject
{
    private readonly ProductService _productService;
    private readonly CartService _cartService;

    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private int cartItemCount;

    public ProductDetailViewModel()
    {
        _productService = ServiceLocator.ProductService;
        _cartService = ServiceLocator.CartService;
    }

    public async Task LoadProductAsync(Product? product)
    {
        if (product == null) return;

        IsLoading = true;
        try
        {
            Product = await _productService.GetProductByIdAsync(product.Id);
            await UpdateCartCount();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading product: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddToCart()
    {
        if (Product == null) return;

        IsLoading = true;
        try
        {
            var success = await _cartService.AddToCartAsync(Product.Id);
            if (success)
            {
                await UpdateCartCount();
                ToastService.Instance.ShowSuccess($"{Product.Name} added to cart!");
            }
            else
            {
                ToastService.Instance.ShowError("Failed to add to cart");
            }
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("Failed to add to cart");
            System.Diagnostics.Debug.WriteLine($"Error adding to cart: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void NavigateToEdit()
    {
        if (Product == null) return;

        var pageType = typeof(Uno_Platform.Views.ProductEditPage);
        ServiceLocator.NavigationService.NavigateTo(pageType, Product);
    }

    [RelayCommand]
    private void GoBack()
    {
        ServiceLocator.NavigationService.GoBack();
    }

    [RelayCommand]
    private void NavigateToCart()
    {
        var pageType = typeof(Uno_Platform.Views.CartPage);
        ServiceLocator.NavigationService.NavigateTo(pageType);
    }

    [RelayCommand]
    private async Task DeleteProduct()
    {
        if (Product == null) return;

        IsLoading = true;
        try
        {
            var success = await _productService.DeleteProductAsync(Product.Id);
            if (success)
            {
                ToastService.Instance.ShowSuccess($"{Product.Name} deleted successfully");
                await Task.Delay(500); // Small delay for user to see success message
                GoBack();
            }
            else
            {
                ToastService.Instance.ShowError("Failed to delete product");
            }
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("Failed to delete product");
            System.Diagnostics.Debug.WriteLine($"Error deleting product: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
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
