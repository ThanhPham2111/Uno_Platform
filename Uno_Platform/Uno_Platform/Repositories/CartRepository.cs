using Uno_Platform.Database;
using Uno_Platform.Models;

namespace Uno_Platform.Repositories;

public class CartRepository : ICartRepository
{
#if __WASM__
    private readonly InMemoryDbContext _dbContext;

    public CartRepository()
    {
        _dbContext = new InMemoryDbContext();
    }

    public Task<List<CartItem>> GetAllCartItemsAsync()
    {
        return Task.FromResult(_dbContext.GetAllCartItems());
    }

    public Task<CartItem?> GetCartItemByProductIdAsync(int productId)
    {
        return Task.FromResult(_dbContext.GetCartItemByProductId(productId));
    }

    public Task<bool> AddCartItemAsync(CartItem item)
    {
        item.ProductImage = "Assets/img/caby.png"; // Always use default image
        _dbContext.AddCartItem(item);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateCartItemAsync(CartItem item)
    {
        item.ProductImage = "Assets/img/caby.png"; // Always use default image
        _dbContext.UpdateCartItem(item);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteCartItemAsync(int id)
    {
        _dbContext.DeleteCartItem(id);
        return Task.FromResult(true);
    }

    public Task<bool> ClearCartAsync()
    {
        _dbContext.ClearCart();
        return Task.FromResult(true);
    }

    public Task<int> GetCartItemCountAsync()
    {
        var items = _dbContext.GetAllCartItems();
        var count = items.Sum(item => item.Quantity);
        return Task.FromResult(count);
    }
#else
    private readonly AppDbContext _dbContext;

    public CartRepository()
    {
        _dbContext = new AppDbContext();
    }

    public Task<List<CartItem>> GetAllCartItemsAsync()
    {
        try
        {
            var items = _dbContext.Connection.Table<CartItem>().ToList();
            // Ensure all items use default image
            foreach (var item in items)
            {
                item.ProductImage = "Assets/img/caby.png";
            }
            return Task.FromResult(items);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting cart items: {ex.Message}");
            return Task.FromResult(new List<CartItem>());
        }
    }

    public Task<CartItem?> GetCartItemByProductIdAsync(int productId)
    {
        try
        {
            var item = _dbContext.Connection.Table<CartItem>().FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                item.ProductImage = "Assets/img/caby.png";
            }
            return Task.FromResult(item);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting cart item: {ex.Message}");
            return Task.FromResult<CartItem?>(null);
        }
    }

    public Task<bool> AddCartItemAsync(CartItem item)
    {
        try
        {
            item.ProductImage = "Assets/img/caby.png"; // Always use default image
            int result = _dbContext.Connection.Insert(item);
            return Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding cart item: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<bool> UpdateCartItemAsync(CartItem item)
    {
        try
        {
            item.ProductImage = "Assets/img/caby.png"; // Always use default image
            int result = _dbContext.Connection.Update(item);
            return Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating cart item: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<bool> DeleteCartItemAsync(int id)
    {
        try
        {
            int result = _dbContext.Connection.Delete<CartItem>(id);
            return Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting cart item: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<bool> ClearCartAsync()
    {
        try
        {
            _dbContext.Connection.DeleteAll<CartItem>();
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error clearing cart: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public async Task<int> GetCartItemCountAsync()
    {
        try
        {
            var items = await GetAllCartItemsAsync();
            var count = items.Sum(item => item.Quantity);
            return count;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting cart count: {ex.Message}");
            return 0;
        }
    }
#endif
}
