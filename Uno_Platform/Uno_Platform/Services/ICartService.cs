using Uno_Platform.Models;

namespace Uno_Platform.Services;

public interface ICartService
{
    event EventHandler<int>? CartCountChanged;
    Task<List<CartItem>> GetCartItemsAsync();
    Task<int> GetCartItemCountAsync();
    Task<bool> AddToCartAsync(int productId);
    Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    Task<bool> RemoveFromCartAsync(int cartItemId);
    Task<bool> ClearCartAsync();
    Task<decimal> GetTotalPriceAsync();
    Task UpdateProductInCartAsync(Product product);
}
