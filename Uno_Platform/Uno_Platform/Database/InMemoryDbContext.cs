using Uno_Platform.Models;

namespace Uno_Platform.Database;

#if __WASM__
public class InMemoryDbContext
{
    private static List<Product> _products = new();
    private static int _nextId = 1;

    public List<Product> Products => _products;

    public void AddProduct(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existing != null)
        {
            var index = _products.IndexOf(existing);
            _products[index] = product;
        }
    }

    public void DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public List<Product> GetAllProducts()
    {
        return _products.ToList();
    }
}
#endif

