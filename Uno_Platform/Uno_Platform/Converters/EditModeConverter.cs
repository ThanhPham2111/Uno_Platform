using Microsoft.UI.Xaml.Data;

namespace Uno_Platform.Converters;

public class EditModeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "Edit Product" : "Create Product";
        }
        return "Create Product";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

