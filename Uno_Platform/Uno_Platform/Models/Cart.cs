using System.Collections.ObjectModel;

namespace Uno_Platform.Models;

public class Cart
{
    public ObservableCollection<CartItem> Items { get; set; } = new();
    
    public int TotalItems => Items.Sum(item => item.Quantity);
    
    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    
    public bool IsEmpty => Items.Count == 0;
}

