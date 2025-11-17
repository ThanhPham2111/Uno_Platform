namespace Uno_Platform.Models;

public class CategoryWithCount
{
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    
    public string DisplayText => $"{Name} ({Count})";
}
