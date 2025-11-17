using Uno_Platform.Models;
using Uno_Platform.Repositories;

namespace Uno_Platform.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllProductsAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _repository.GetProductByIdAsync(id);
    }

    public async Task<List<Product>> SearchProductsAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return await GetAllProductsAsync();
        
        return await _repository.SearchProductsAsync(keyword);
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return await GetAllProductsAsync();
        
        return await _repository.GetProductsByCategoryAsync(category);
    }

    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _repository.GetProductsByPriceRangeAsync(minPrice, maxPrice);
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        if (!ValidateProduct(product))
            return false;
        
        return await _repository.AddProductAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        if (!ValidateProduct(product))
            return false;
        
        return await _repository.UpdateProductAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _repository.DeleteProductAsync(id);
    }

    public async Task<List<string>> GetAllCategoriesAsync()
    {
        return await _repository.GetAllCategoriesAsync();
    }

    private bool ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || product.Name.Length < 2)
            return false;
        
        if (product.Price < 0)
            return false;
        
        if (string.IsNullOrWhiteSpace(product.Description))
            return false;
        
        if (string.IsNullOrWhiteSpace(product.Category))
            return false;
        
        return true;
    }
}
