using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Uno_Platform.Services;

public class NavigationService
{
    private Frame? _frame;

    public void Initialize(Frame frame)
    {
        _frame = frame;
    }

    public bool NavigateTo(Type pageType, object? parameter = null)
    {
        if (_frame == null)
        {
            return false;
        }

        return _frame.Navigate(pageType, parameter);
    }

    public bool CanGoBack()
    {
        return _frame?.CanGoBack ?? false;
    }

    public void GoBack()
    {
        if (_frame?.CanGoBack == true)
        {
            _frame.GoBack();
        }
    }

    public void ClearBackStack()
    {
        if (_frame != null)
        {
            while (_frame.CanGoBack)
            {
                _frame.BackStack.RemoveAt(_frame.BackStack.Count - 1);
            }
        }
    }
}

