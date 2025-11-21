using Microsoft.UI.Xaml.Data;

namespace Uno_Platform.Converters;

public class StringToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
