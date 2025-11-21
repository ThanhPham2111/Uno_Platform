using Uno_Platform.Database;
using Uno_Platform.Models;
#if !__WASM__
using Microsoft.EntityFrameworkCore;
#endif

namespace Uno_Platform.Repositories;

public class CartRepository : ICartRepository
{
#if !__WASM__
    private readonly EfAppDbContext _dbContext;
    private bool _isInitialized = false;
    private readonly SemaphoreSlim _initLock = new SemaphoreSlim(1, 1);

    public CartRepository()
    {
        _dbContext = new EfAppDbContext();
        System.Diagnostics.Debug.WriteLine("=== CartRepository: Using EF Core SQLite ===");
    }

    private async Task EnsureDatabaseInitializedAsync()
    {
        if (_isInitialized) return;

        await _initLock.WaitAsync();
        try
        {
            if (!_isInitialized)
            {
                await _dbContext.Database.EnsureCreatedAsync();
                _isInitialized = true;
                System.Diagnostics.Debug.WriteLine("=== Database initialized successfully ===");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error initializing database: {ex.Message}");
            throw;
        }
        finally
        {
            _initLock.Release();
        }
    }

    public async Task<List<CartItem>> GetAllCartItemsAsync()
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            return await _dbContext.CartItems.ToListAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting cart items: {ex.Message}");
            return new List<CartItem>();
        }
    }

    public async Task<CartItem?> GetCartItemByProductIdAsync(int productId)
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            return await _dbContext.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting cart item by product ID: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddCartItemAsync(CartItem item)
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            item.ProductImage = "Assets/img/caby.png"; // Always use default image
            _dbContext.CartItems.Add(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding cart item: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCartItemAsync(CartItem item)
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            item.ProductImage = "Assets/img/caby.png"; // Always use default image
            _dbContext.CartItems.Update(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating cart item: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCartItemAsync(int id)
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            var item = await _dbContext.CartItems.FindAsync(id);
            if (item != null)
            {
                _dbContext.CartItems.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting cart item: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ClearCartAsync()
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            var allItems = await _dbContext.CartItems.ToListAsync();
            _dbContext.CartItems.RemoveRange(allItems);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error clearing cart: {ex.Message}");
            return false;
        }
    }

    public async Task<int> GetCartItemCountAsync()
    {
        await EnsureDatabaseInitializedAsync();
        try
        {
            var items = await _dbContext.CartItems.ToListAsync();
            return items.Sum(item => item.Quantity);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting cart count: {ex.Message}");
            return 0;
        }
    }
#else
    // WebAssembly: Sử dụng InMemory
    private readonly InMemoryDbContext _dbContext;

    public CartRepository()
    {
        _dbContext = new InMemoryDbContext();
        System.Diagnostics.Debug.WriteLine("=== CartRepository: Using InMemoryDbContext (WASM) ===");
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
        item.ProductImage = "Assets/img/caby.png";
        _dbContext.AddCartItem(item);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateCartItemAsync(CartItem item)
    {
        item.ProductImage = "Assets/img/caby.png";
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
        return Task.FromResult(items.Sum(item => item.Quantity));
    }
#endif
}
