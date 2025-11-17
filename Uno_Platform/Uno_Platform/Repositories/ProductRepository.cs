using Uno_Platform.Database;
using Uno_Platform.Models;

namespace Uno_Platform.Repositories;

public class ProductRepository : IProductRepository
{
#if __WASM__
    private readonly InMemoryDbContext _dbContext;

    public ProductRepository()
    {
        _dbContext = new InMemoryDbContext();
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
        return Task.FromResult(_dbContext.GetAllProducts());
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        return Task.FromResult(_dbContext.GetProductById(id));
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
#else
    private readonly AppDbContext _dbContext;

    public ProductRepository()
    {
        _dbContext = new AppDbContext();
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
        try
        {
            var products = _dbContext.Connection.Table<Product>().ToList();
            // Ensure all products use default image
            foreach (var product in products)
            {
                product.Image = "Assets/img/caby.png";
            }
            return Task.FromResult(products);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting products: {ex.Message}");
            return Task.FromResult(new List<Product>());
        }
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            var product = _dbContext.Connection.Table<Product>().FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Image = "Assets/img/caby.png";
            }
            return Task.FromResult(product);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting product: {ex.Message}");
            return Task.FromResult<Product?>(null);
        }
    }

    public async Task<List<Product>> SearchProductsAsync(string keyword)
    {
        try
        {
            var all = await GetAllProductsAsync();
            if (string.IsNullOrWhiteSpace(keyword))
                return all;

            var lowerKeyword = keyword.ToLowerInvariant();
            var results = all.Where(p =>
                p.Name.ToLowerInvariant().Contains(lowerKeyword) ||
                p.Category.ToLowerInvariant().Contains(lowerKeyword) ||
                p.Description.ToLowerInvariant().Contains(lowerKeyword)
            ).ToList();
            return results;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error searching products: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        try
        {
            var all = await GetAllProductsAsync();
            var results = all.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            return results;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting products by category: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        try
        {
            var all = await GetAllProductsAsync();
            var results = all.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            return results;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting products by price range: {ex.Message}");
            return new List<Product>();
        }
    }

    public Task<bool> AddProductAsync(Product product)
    {
        try
        {
            product.Image = "Assets/img/caby.png"; // Always use default image
            int result = _dbContext.Connection.Insert(product);
            return Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding product: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<bool> UpdateProductAsync(Product product)
    {
        try
        {
            product.Image = "Assets/img/caby.png"; // Always use default image
            int result = _dbContext.Connection.Update(product);
            return Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating product: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            int result = _dbContext.Connection.Delete<Product>(id);
            return Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting product: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public async Task<List<string>> GetAllCategoriesAsync()
    {
        try
        {
            var all = await GetAllProductsAsync();
            var categories = all.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
            return categories;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting categories: {ex.Message}");
            return new List<string>();
        }
    }
#endif
}
