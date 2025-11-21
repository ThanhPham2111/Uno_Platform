using Uno_Platform.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Uno_Platform.Views;

public sealed partial class LoginPage : Page
{
    public LoginViewModel ViewModel { get; }

    public LoginPage()
    {
        this.InitializeComponent();
        ViewModel = new LoginViewModel();
        this.DataContext = ViewModel;
    }

    private void PasswordBox_PasswordChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            ViewModel.Password = passwordBox.Password;
        }
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Navigate back to Home
        Frame.Navigate(typeof(Uno_Platform.Views.ProductListPage));
    }
}

