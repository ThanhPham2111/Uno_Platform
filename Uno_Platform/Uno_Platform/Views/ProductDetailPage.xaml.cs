using Uno_Platform.ViewModels;
using Uno_Platform.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Uno_Platform.Views;

public sealed partial class ProductDetailPage : Page
{
    public ProductDetailViewModel ViewModel { get; }

    public ProductDetailPage()
    {
        this.InitializeComponent();
        ViewModel = new ProductDetailViewModel();
        this.DataContext = ViewModel;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        PageEnterAnimation.Begin();
        if (e.Parameter is Product product)
        {
            _ = ViewModel.LoadProductAsync(product);
        }
        else
        {
            // If no product provided, navigate back
            ViewModel.GoBackCommand.Execute(null);
            return;
        }
        ImageFadeIn.Begin();
    }
}
