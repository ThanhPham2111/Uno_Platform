#if !__WASM__
using SQLite;
#endif

namespace Uno_Platform.Models;

public class CartItem
{
#if !__WASM__
    [PrimaryKey, AutoIncrement]
#endif
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public string ProductImage { get; set; } = "Assets/img/caby.png";
    public string ProductCategory { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public DateTime AddedAt { get; set; } = DateTime.Now;

    public decimal TotalPrice => ProductPrice * Quantity;
}
