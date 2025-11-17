using Uno_Platform.Models;

namespace Uno_Platform.Repositories;

public interface ICartRepository
{
    Task<List<CartItem>> GetAllCartItemsAsync();
    Task<CartItem?> GetCartItemByProductIdAsync(int productId);
    Task<bool> AddCartItemAsync(CartItem item);
    Task<bool> UpdateCartItemAsync(CartItem item);
    Task<bool> DeleteCartItemAsync(int id);
    Task<bool> ClearCartAsync();
    Task<int> GetCartItemCountAsync();
}
