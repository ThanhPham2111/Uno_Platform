using Uno_Platform.ViewModels;
using Uno_Platform.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Uno_Platform.Views;

public sealed partial class ProductEditPage : Page
{
    public ProductEditViewModel ViewModel { get; }

    public ProductEditPage()
    {
        this.InitializeComponent();
        ViewModel = new ProductEditViewModel();
        this.DataContext = ViewModel;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is Product product)
        {
            _ = ViewModel.LoadProductAsync(product);
        }
        else
        {
            _ = ViewModel.LoadProductAsync(null); // New product mode
        }
    }


    private void PriceTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
    {
        if (sender != null && !string.IsNullOrWhiteSpace(sender.Text))
        {
            if (decimal.TryParse(sender.Text, out decimal price))
            {
                ViewModel.Price = price;
            }
        }
    }
}

