using Microsoft.UI.Xaml.Controls;
using Uno_Platform.ViewModels;

namespace Uno_Platform.Views;

public sealed partial class OrderSuccessPage : Page
{
    public OrderSuccessViewModel ViewModel { get; }

    public OrderSuccessPage()
    {
        this.InitializeComponent();
        ViewModel = new OrderSuccessViewModel();
        this.DataContext = ViewModel;
    }
}
