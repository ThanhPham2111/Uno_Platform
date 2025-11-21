using Uno_Platform.Database;
using Uno_Platform.Models;
using Uno_Platform.Services;

namespace Uno_Platform.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InMemoryDbContext _dbContext;
    private readonly ApiService _apiService;

    public ProductRepository()
    {
        _dbContext = new InMemoryDbContext();
        _apiService = ServiceLocator.ApiService;
        System.Diagnostics.Debug.WriteLine("=== ProductRepository: Initialized with API Service ===");
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        // Ưu tiên gọi API, nếu thất bại thì dùng InMemory
        try
        {
            var products = await _apiService.GetProductsAsync();
            if (products != null && products.Any())
            {
                System.Diagnostics.Debug.WriteLine($"=== ProductRepository: Got {products.Count} products from API ===");
                return products;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"=== ProductRepository: API call failed, using fallback: {ex.Message} ===");
        }

        // Fallback: dùng InMemory data
        var fallbackProducts = _dbContext.GetAllProducts();
        System.Diagnostics.Debug.WriteLine($"=== ProductRepository: Using fallback, returned {fallbackProducts.Count} products. First item: {fallbackProducts.FirstOrDefault()?.Name} ===");
        return fallbackProducts;
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        var p = _dbContext.GetProductById(id);
        System.Diagnostics.Debug.WriteLine($"=== ProductRepository: GetProductById({id}) returned {p?.Name} Price={p?.Price} ===");
        return Task.FromResult(p);
    }

    public Task<List<Product>> SearchProductsAsync(string keyword)
    {
        var all = _dbContext.GetAllProducts();
        if (string.IsNullOrWhiteSpace(keyword))
            return Task.FromResult(all);

        var lowerKeyword = keyword.ToLowerInvariant();
        var results = all.Where(p =>
            p.Name.ToLowerInvariant().Contains(lowerKeyword) ||
            p.Category.ToLowerInvariant().Contains(lowerKeyword) ||
            p.Description.ToLowerInvariant().Contains(lowerKeyword)
        ).ToList();
        return Task.FromResult(results);
    }

    public Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        var all = _dbContext.GetAllProducts();
        var results = all.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        return Task.FromResult(results);
    }

    public Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var all = _dbContext.GetAllProducts();
        var results = all.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        return Task.FromResult(results);
    }

    public Task<bool> AddProductAsync(Product product)
    {
        product.Image = "Assets/img/caby.png"; // Always use default image
        _dbContext.AddProduct(product);
        System.Diagnostics.Debug.WriteLine($"=== ProductRepository: Added product {product.Name} ===");
        return Task.FromResult(true);
    }

    public Task<bool> UpdateProductAsync(Product product)
    {
        product.Image = "Assets/img/caby.png"; // Always use default image
        _dbContext.UpdateProduct(product);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        _dbContext.DeleteProduct(id);
        return Task.FromResult(true);
    }

    public Task<List<string>> GetAllCategoriesAsync()
    {
        var all = _dbContext.GetAllProducts();
        var categories = all.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
        return Task.FromResult(categories);
    }
}
