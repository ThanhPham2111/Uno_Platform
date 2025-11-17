using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Uno_Platform.Views;

public sealed partial class AppShell : Page
{
    private string _currentTab = "Home";

    public AppShell()
    {
        this.InitializeComponent();
        InitializeNavigation();
    }

    private void InitializeNavigation()
    {
        // Set initial page
        NavigateToHome();
    }

    private void NavigateToHome()
    {
        if (_currentTab == "Home" && ContentFrame.Content is ProductListPage) return;
        
        _currentTab = "Home";
        if (!(ContentFrame.Content is ProductListPage))
        {
            ContentFrame.Navigate(typeof(ProductListPage));
        }
        UpdateTabIndicators("Home");
        PlayPageTransition();
    }

    private void NavigateToCart()
    {
        if (_currentTab == "Cart" && ContentFrame.Content is CartPage) return;
        
        _currentTab = "Cart";
        if (!(ContentFrame.Content is CartPage))
        {
            ContentFrame.Navigate(typeof(CartPage));
        }
        UpdateTabIndicators("Cart");
        PlayPageTransition();
    }

    private void NavigateToSettings()
    {
        if (_currentTab == "Settings" && ContentFrame.Content is SettingsPage) return;
        
        _currentTab = "Settings";
        if (!(ContentFrame.Content is SettingsPage))
        {
            ContentFrame.Navigate(typeof(SettingsPage));
        }
        UpdateTabIndicators("Settings");
        PlayPageTransition();
    }

    private void UpdateTabIndicators(string activeTab)
    {
        // Reset all indicators
        HomeIndicator.Opacity = 0;
        CartIndicator.Opacity = 0;
        SettingsIndicator.Opacity = 0;

        var grayColor = Windows.UI.Color.FromArgb(255, 128, 128, 128);
        HomeIcon.Foreground = new SolidColorBrush(grayColor);
        CartIcon.Foreground = new SolidColorBrush(grayColor);
        SettingsIcon.Foreground = new SolidColorBrush(grayColor);

        // Activate selected tab
        switch (activeTab)
        {
            case "Home":
                HomeIndicator.Opacity = 1;
                HomeIcon.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 99, 102, 241)); // Primary color
                break;
            case "Cart":
                CartIndicator.Opacity = 1;
                CartIcon.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 99, 102, 241)); // Primary color
                break;
            case "Settings":
                SettingsIndicator.Opacity = 1;
                SettingsIcon.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 99, 102, 241)); // Primary color
                break;
        }
    }

    private void PlayPageTransition()
    {
        // Reset animation
        ContentFrame.Opacity = 0;
        var transform = ContentFrame.RenderTransform as Microsoft.UI.Xaml.Media.TranslateTransform;
        if (transform != null)
        {
            transform.X = 20;
        }

        // Start animation
        var animation = Resources["PageEnterAnimation"] as Microsoft.UI.Xaml.Media.Animation.Storyboard;
        animation?.Begin();
    }

    private void HomeTab_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        NavigateToHome();
    }

    private void CartTab_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        NavigateToCart();
    }

    private void SettingsTab_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        NavigateToSettings();
    }

    private void SearchButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Navigate to home and trigger search if on product list page
        NavigateToHome();
        
        // This will be handled by the ProductListPage
        // For now, just navigate to home tab
    }

    public void UpdateCartBadge(int count)
    {
        if (count > 0)
        {
            CartBadge.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            CartBadgeText.Text = count > 99 ? "99+" : count.ToString();
        }
        else
        {
            CartBadge.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
    }

    public Frame GetContentFrame()
    {
        return ContentFrame;
    }
}

