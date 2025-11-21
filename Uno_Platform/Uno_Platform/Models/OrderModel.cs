using System.Collections.Generic;

namespace Uno_Platform.Models;

public class OrderModel
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public List<OrderItemModel> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
}

public class OrderItemModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

