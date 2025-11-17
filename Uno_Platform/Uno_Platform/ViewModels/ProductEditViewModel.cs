using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Models;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class ProductEditViewModel : ObservableObject
{
    private readonly ProductService _productService;

    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private decimal price;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string category = string.Empty;

    [ObservableProperty]
    private List<string> categories = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool isEditMode;

    public ProductEditViewModel()
    {
        _productService = ServiceLocator.ProductService;
    }

    public async Task LoadProductAsync(Product? product)
    {
        if (product == null)
        {
            // New product mode
            IsEditMode = false;
            Product = new Product();
            await LoadCategories();
            return;
        }

        IsLoading = true;
        try
        {
            System.Diagnostics.Debug.WriteLine($"LoadProductAsync: Loading product with Id: {product.Id}");
            Product = await _productService.GetProductByIdAsync(product.Id);
            if (Product != null)
            {
                System.Diagnostics.Debug.WriteLine($"LoadProductAsync: Product loaded - Name: {Product.Name}, Id: {Product.Id}");
                IsEditMode = true;
                Name = Product.Name;
                Price = Product.Price;
                Description = Product.Description;
                Category = Product.Category;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("LoadProductAsync: Product is null after GetProductByIdAsync");
            }
            await LoadCategories();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading product: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task LoadCategories()
    {
        try
        {
            Categories = await _productService.GetAllCategoriesAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Name) || Name.Length < 2)
        {
            ErrorMessage = "Name must be at least 2 characters";
            return;
        }

        if (Price < 0)
        {
            ErrorMessage = "Price must be positive";
            return;
        }

        if (string.IsNullOrWhiteSpace(Description))
        {
            ErrorMessage = "Description is required";
            return;
        }

        if (string.IsNullOrWhiteSpace(Category))
        {
            ErrorMessage = "Category is required";
            return;
        }

        IsLoading = true;
        try
        {
            if (Product == null)
            {
                Product = new Product();
            }

            Product.Name = Name;
            Product.Price = Price;
            Product.Description = Description;
            Product.Category = Category;
            Product.Image = "Assets/img/caby.png"; // Always use default image

            bool success;
            if (IsEditMode && Product.Id > 0)
            {
                // Update existing product
                success = await _productService.UpdateProductAsync(Product);
            }
            else
            {
                // Add new product
                success = await _productService.AddProductAsync(Product);
            }

            if (success)
            {
                ToastService.Instance.ShowSuccess(IsEditMode ? "Product updated!" : "Product created!");
                await Task.Delay(500); // Small delay for user to see success message
                GoBack();
            }
            else
            {
                ErrorMessage = "Failed to save product";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred while saving";
            System.Diagnostics.Debug.WriteLine($"Error saving product: {ex.Message}");
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
