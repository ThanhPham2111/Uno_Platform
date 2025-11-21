using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    public LoginViewModel()
    {
        _authService = new AuthenticationService();
    }

    [RelayCommand]
    private void Login()
    {
        ErrorMessage = string.Empty;
        IsLoading = true;

        try
        {
            if (_authService.Login(Username, Password))
            {
                // Navigate to ProductListPage (Home)
                var homePageType = typeof(Uno_Platform.Views.ProductListPage);
                ServiceLocator.NavigationService.NavigateTo(homePageType);
            }
            else
            {
                ErrorMessage = "Invalid username or password. Both must be at least 3 characters.";
                ToastService.Instance.ShowError(ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred during login. Please try again.";
            ToastService.Instance.ShowError(ErrorMessage);
            System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

