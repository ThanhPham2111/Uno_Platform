using Uno_Platform.Models;

namespace Uno_Platform.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Product>> SearchProductsAsync(string keyword);
    Task<List<Product>> GetProductsByCategoryAsync(string category);
    Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<bool> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<List<string>> GetAllCategoriesAsync();
}
