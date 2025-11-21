using Uno_Platform.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Uno_Platform.Views;

public sealed partial class CheckoutPage : Page
{
    public CheckoutViewModel ViewModel { get; }

    public CheckoutPage()
    {
        this.InitializeComponent();
        ViewModel = new CheckoutViewModel();
        this.DataContext = ViewModel;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        // Load cart data when page is navigated to
        ViewModel.LoadCartDataCommand.Execute(null);
    }
}

