using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Models;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class ProductDetailViewModel : ObservableObject
{
    private readonly ProductService _productService;

    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private bool isEditMode;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private decimal price;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string image = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    public ProductDetailViewModel()
    {
        _productService = new ProductService();
    }

    public void LoadProduct(Product? product)
    {
        Product = product;
        if (product != null)
        {
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Image = product.Image;
        }
    }

    [RelayCommand]
    private void ToggleEditMode()
    {
        IsEditMode = !IsEditMode;
    }

    [RelayCommand]
    private void SaveProduct()
    {
        if (Product == null) return;

        ErrorMessage = string.Empty;
        IsLoading = true;

        try
        {
            Product.Name = Name;
            Product.Price = Price;
            Product.Description = Description;
            Product.Image = Image;

            bool success = _productService.UpdateProduct(Product);
            if (success)
            {
                IsEditMode = false;
                ToastService.Instance.ShowSuccess("Product updated successfully");
            }
            else
            {
                ErrorMessage = "Failed to update product. Please check your input.";
                ToastService.Instance.ShowError(ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred while saving. Please try again.";
            ToastService.Instance.ShowError(ErrorMessage);
            System.Diagnostics.Debug.WriteLine($"Error saving product: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void DeleteProduct()
    {
        if (Product == null) return;

        IsLoading = true;
        try
        {
            bool success = _productService.DeleteProduct(Product.Id);
            if (success)
            {
                ToastService.Instance.ShowSuccess("Product deleted successfully");
                ServiceLocator.NavigationService.GoBack();
            }
            else
            {
                ToastService.Instance.ShowError("Failed to delete product");
            }
        }
        catch (Exception ex)
        {
            ToastService.Instance.ShowError("An error occurred while deleting. Please try again.");
            System.Diagnostics.Debug.WriteLine($"Error deleting product: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        ServiceLocator.NavigationService.GoBack();
    }
}

