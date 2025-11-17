#if !__WASM__
using SQLite;
#endif

namespace Uno_Platform.Models;

public class Product
{
#if !__WASM__
    [PrimaryKey, AutoIncrement]
#endif
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = "Assets/img/caby.png"; // Always use default image
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Ensure image always points to default
    public string GetImagePath() => "Assets/img/caby.png";
}

