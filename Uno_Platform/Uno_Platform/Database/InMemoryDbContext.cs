using Uno_Platform.Models;

namespace Uno_Platform.Database;

public class InMemoryDbContext
{
    private readonly List<Product> _products = new();
    private readonly List<CartItem> _cartItems = new();
    private int _nextProductId = 1;
    private int _nextCartItemId = 1;

    public List<Product> GetAllProducts() => _products.ToList();

    public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public void AddProduct(Product product)
    {
        product.Id = _nextProductId++;
        product.Image = "Assets/img/caby.png"; // Always use default image
        _products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        product.Image = "Assets/img/caby.png"; // Always use default image
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index >= 0)
        {
            _products[index] = product;
        }
    }

    public void DeleteProduct(int id)
    {
        _products.RemoveAll(p => p.Id == id);
    }

    public List<CartItem> GetAllCartItems() => _cartItems.ToList();

    public CartItem? GetCartItemByProductId(int productId) => _cartItems.FirstOrDefault(c => c.ProductId == productId);

    public void AddCartItem(CartItem item)
    {
        item.Id = _nextCartItemId++;
        item.ProductImage = "Assets/img/caby.png"; // Always use default image
        _cartItems.Add(item);
    }

    public void UpdateCartItem(CartItem item)
    {
        item.ProductImage = "Assets/img/caby.png"; // Always use default image
        var index = _cartItems.FindIndex(c => c.Id == item.Id);
        if (index >= 0)
        {
            _cartItems[index] = item;
        }
    }

    public void DeleteCartItem(int id)
    {
        _cartItems.RemoveAll(c => c.Id == id);
    }

    public void ClearCart()
    {
        _cartItems.Clear();
    }
}
