using Uno_Platform.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Uno_Platform.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel { get; }

    public HomePage()
    {
        this.InitializeComponent();
        ViewModel = new HomeViewModel();
        this.DataContext = ViewModel;
    }
}

