using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Uno_Platform.Models;

namespace Uno_Platform.Converters;

public class CategorySelectedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string categoryName && parameter is string selectedCategory)
        {
            bool isSelected = categoryName == selectedCategory;
            
            if (targetType == typeof(Brush))
            {
                // Return background brush
                if (isSelected)
                {
                    return new SolidColorBrush(Microsoft.UI.Colors.Transparent); // Will be set via style
                }
                return Application.Current.Resources["AcrylicBrush"] as Brush ?? new SolidColorBrush(Microsoft.UI.Colors.Transparent);
            }
            else if (targetType == typeof(bool))
            {
                return isSelected;
            }
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
