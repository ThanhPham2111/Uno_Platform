using Uno_Platform.Models;

namespace Uno_Platform.Services;

public class ProductService
{
    private readonly DatabaseService _databaseService;

    public ProductService()
    {
        _databaseService = new DatabaseService();
    }

    public List<Product> SearchProducts(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return GetAllProducts();
        }

        var allProducts = GetAllProducts();
        var lowerKeyword = keyword.ToLowerInvariant();

        return allProducts.Where(p =>
            p.Name.ToLowerInvariant().Contains(lowerKeyword) ||
            p.Description.ToLowerInvariant().Contains(lowerKeyword)
        ).ToList();
    }

    public List<Product> FilterByPrice(decimal? minPrice, decimal? maxPrice)
    {
        var allProducts = GetAllProducts();

        if (minPrice.HasValue && maxPrice.HasValue)
        {
            return allProducts.Where(p => p.Price >= minPrice.Value && p.Price <= maxPrice.Value).ToList();
        }

        if (minPrice.HasValue)
        {
            return allProducts.Where(p => p.Price >= minPrice.Value).ToList();
        }

        if (maxPrice.HasValue)
        {
            return allProducts.Where(p => p.Price <= maxPrice.Value).ToList();
        }

        return allProducts;
    }

    public List<Product> GetAllProducts()
    {
        return _databaseService.GetAllProducts();
    }

    public Product? GetProductById(int id)
    {
        return _databaseService.GetProductById(id);
    }

    public bool AddProduct(Product product)
    {
        if (!ValidateProduct(product))
        {
            return false;
        }

        return _databaseService.AddProduct(product);
    }

    public bool UpdateProduct(Product product)
    {
        if (!ValidateProduct(product))
        {
            return false;
        }

        return _databaseService.UpdateProduct(product);
    }

    public bool DeleteProduct(int id)
    {
        return _databaseService.DeleteProduct(id);
    }

    private bool ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return false;
        }

        if (product.Name.Length < 2)
        {
            return false;
        }

        if (product.Price < 0)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(product.Description))
        {
            return false;
        }

        return true;
    }
}

