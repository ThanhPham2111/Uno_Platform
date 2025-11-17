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
        ViewModel.LoadProducts();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        // Update empty state when products change
        if (ViewModel.Products != null)
        {
            UpdateEmptyState();
        }
    }

    private void ProductButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Product product)
        {
            ViewModel.NavigateToProductDetailCommand.Execute(product);
        }
    }

    private void UpdateEmptyState()
    {
        if (EmptyStateText != null && ViewModel != null)
        {
            EmptyStateText.Visibility = ViewModel.Products.Count == 0 
                ? Microsoft.UI.Xaml.Visibility.Visible 
                : Microsoft.UI.Xaml.Visibility.Collapsed;
        }
    }

    private void MinPriceTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
    {
        if (sender != null && !string.IsNullOrWhiteSpace(sender.Text))
        {
            if (decimal.TryParse(sender.Text, out decimal price))
            {
                ViewModel.MinPrice = price;
            }
        }
        else
        {
            ViewModel.MinPrice = null;
        }
    }

    private void MaxPriceTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
    {
        if (sender != null && !string.IsNullOrWhiteSpace(sender.Text))
        {
            if (decimal.TryParse(sender.Text, out decimal price))
            {
                ViewModel.MaxPrice = price;
            }
        }
        else
        {
            ViewModel.MaxPrice = null;
        }
    }
}

