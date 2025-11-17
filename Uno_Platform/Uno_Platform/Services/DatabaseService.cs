using Uno_Platform.Database;
using Uno_Platform.Models;

namespace Uno_Platform.Services;

public class DatabaseService
{
#if __WASM__
    private readonly InMemoryDbContext _dbContext;

    public DatabaseService()
    {
        _dbContext = new InMemoryDbContext();
    }

    public List<Product> GetAllProducts()
    {
        return _dbContext.GetAllProducts();
    }

    public Product? GetProductById(int id)
    {
        return _dbContext.GetProductById(id);
    }

    public bool AddProduct(Product product)
    {
        try
        {
            _dbContext.AddProduct(product);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding product: {ex.Message}");
            return false;
        }
    }

    public bool UpdateProduct(Product product)
    {
        try
        {
            _dbContext.UpdateProduct(product);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating product: {ex.Message}");
            return false;
        }
    }

    public bool DeleteProduct(int id)
    {
        try
        {
            _dbContext.DeleteProduct(id);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting product: {ex.Message}");
            return false;
        }
    }

    public void SeedSampleData()
    {
        try
        {
            var existingProducts = GetAllProducts();
            if (existingProducts.Count == 0)
            {
                var sampleProducts = new List<Product>
                {
                    new Product { Name = "Laptop", Price = 999.99m, Description = "High-performance laptop for work and gaming", Image = "💻" },
                    new Product { Name = "Smartphone", Price = 699.99m, Description = "Latest smartphone with advanced features", Image = "📱" },
                    new Product { Name = "Headphones", Price = 199.99m, Description = "Wireless noise-cancelling headphones", Image = "🎧" },
                    new Product { Name = "Tablet", Price = 499.99m, Description = "10-inch tablet perfect for reading and browsing", Image = "📱" },
                    new Product { Name = "Smartwatch", Price = 299.99m, Description = "Fitness tracking smartwatch", Image = "⌚" }
                };

                foreach (var product in sampleProducts)
                {
                    AddProduct(product);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error seeding data: {ex.Message}");
        }
    }
#else
    private readonly AppDbContext _dbContext;

    public DatabaseService()
    {
        _dbContext = new AppDbContext();
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            return _dbContext.Connection.Table<Product>().ToList();
        }
        catch (SQLite.SQLiteException ex)
        {
            System.Diagnostics.Debug.WriteLine($"SQLite error getting all products: {ex.Message}");
            ToastService.Instance.ShowError("Database error. Please try again.");
            return new List<Product>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting all products: {ex.Message}");
            ToastService.Instance.ShowError("Error loading products.");
            return new List<Product>();
        }
    }

    public Product? GetProductById(int id)
    {
        try
        {
            return _dbContext.Connection.Table<Product>().FirstOrDefault(p => p.Id == id);
        }
        catch (SQLite.SQLiteException ex)
        {
            System.Diagnostics.Debug.WriteLine($"SQLite error getting product by ID: {ex.Message}");
            ToastService.Instance.ShowError("Database error.");
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting product by ID: {ex.Message}");
            ToastService.Instance.ShowError("Error loading product.");
            return null;
        }
    }

    public bool AddProduct(Product product)
    {
        try
        {
            int result = _dbContext.Connection.Insert(product);
            return result > 0;
        }
        catch (SQLite.SQLiteException ex)
        {
            System.Diagnostics.Debug.WriteLine($"SQLite error adding product: {ex.Message}");
            ToastService.Instance.ShowError("Database error. Product not added.");
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding product: {ex.Message}");
            ToastService.Instance.ShowError("Error adding product.");
            return false;
        }
    }

    public bool UpdateProduct(Product product)
    {
        try
        {
            int result = _dbContext.Connection.Update(product);
            return result > 0;
        }
        catch (SQLite.SQLiteException ex)
        {
            System.Diagnostics.Debug.WriteLine($"SQLite error updating product: {ex.Message}");
            ToastService.Instance.ShowError("Database error. Product not updated.");
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating product: {ex.Message}");
            ToastService.Instance.ShowError("Error updating product.");
            return false;
        }
    }

    public bool DeleteProduct(int id)
    {
        try
        {
            int result = _dbContext.Connection.Delete<Product>(id);
            return result > 0;
        }
        catch (SQLite.SQLiteException ex)
        {
            System.Diagnostics.Debug.WriteLine($"SQLite error deleting product: {ex.Message}");
            ToastService.Instance.ShowError("Database error. Product not deleted.");
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting product: {ex.Message}");
            ToastService.Instance.ShowError("Error deleting product.");
            return false;
        }
    }

    public void SeedSampleData()
    {
        try
        {
            var existingProducts = GetAllProducts();
            if (existingProducts.Count == 0)
            {
                var sampleProducts = new List<Product>
                {
                    new Product { Name = "Laptop", Price = 999.99m, Description = "High-performance laptop for work and gaming", Image = "💻" },
                    new Product { Name = "Smartphone", Price = 699.99m, Description = "Latest smartphone with advanced features", Image = "📱" },
                    new Product { Name = "Headphones", Price = 199.99m, Description = "Wireless noise-cancelling headphones", Image = "🎧" },
                    new Product { Name = "Tablet", Price = 499.99m, Description = "10-inch tablet perfect for reading and browsing", Image = "📱" },
                    new Product { Name = "Smartwatch", Price = 299.99m, Description = "Fitness tracking smartwatch", Image = "⌚" }
                };

                foreach (var product in sampleProducts)
                {
                    AddProduct(product);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error seeding data: {ex.Message}");
        }
    }
#endif
}

