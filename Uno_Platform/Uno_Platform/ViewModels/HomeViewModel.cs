using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;

    [ObservableProperty]
    private string welcomeMessage = "Welcome to Uno Platform App!";

    public HomeViewModel()
    {
        _authService = new AuthenticationService();
        UpdateWelcomeMessage();
    }

    private void UpdateWelcomeMessage()
    {
        var currentUser = _authService.CurrentUser;
        if (!string.IsNullOrWhiteSpace(currentUser))
        {
            WelcomeMessage = $"Welcome, {currentUser}!";
        }
    }

    [RelayCommand]
    private void NavigateToProducts()
    {
        var productListPageType = typeof(Uno_Platform.Views.ProductListPage);
        ServiceLocator.NavigationService.NavigateTo(productListPageType);
    }

    [RelayCommand]
    private void Logout()
    {
        _authService.Logout();
        ToastService.Instance.ShowMessage("Logged out successfully");
        var loginPageType = typeof(Uno_Platform.Views.LoginPage);
        ServiceLocator.NavigationService.ClearBackStack();
        ServiceLocator.NavigationService.NavigateTo(loginPageType);
    }
}

