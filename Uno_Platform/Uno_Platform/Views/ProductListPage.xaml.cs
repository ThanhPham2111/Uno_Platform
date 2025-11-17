using Uno_Platform.ViewModels;
using Uno_Platform.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Uno_Platform.Views;

public sealed partial class ProductListPage : Page
{
    public ProductListViewModel ViewModel { get; }

    public ProductListPage()
    {
        this.InitializeComponent();
        ViewModel = new ProductListViewModel();
        this.DataContext = ViewModel;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        PageEnterAnimation.Begin();
        UpdateEmptyState();
        ViewModel.RefreshCommand.Execute(null);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        UpdateEmptyState();
    }

    private void UpdateEmptyState()
    {
        if (EmptyStateBorder != null && ViewModel != null)
        {
            EmptyStateBorder.Visibility = ViewModel.FilteredProducts.Count == 0
                ? Microsoft.UI.Xaml.Visibility.Visible
                : Microsoft.UI.Xaml.Visibility.Collapsed;
        }
    }

    private void CategoryButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string category)
        {
            ViewModel.SelectedCategory = category;
        }
    }
}
