using Microsoft.UI.Xaml.Controls;

namespace Uno_Platform.Controls;

public sealed partial class LoadingIndicator : UserControl
{
    public static readonly Microsoft.UI.Xaml.DependencyProperty IsLoadingProperty =
        Microsoft.UI.Xaml.DependencyProperty.Register(
            nameof(IsLoading),
            typeof(bool),
            typeof(LoadingIndicator),
            new Microsoft.UI.Xaml.PropertyMetadata(false));

    public static readonly Microsoft.UI.Xaml.DependencyProperty MessageProperty =
        Microsoft.UI.Xaml.DependencyProperty.Register(
            nameof(Message),
            typeof(string),
            typeof(LoadingIndicator),
            new Microsoft.UI.Xaml.PropertyMetadata(string.Empty));

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public LoadingIndicator()
    {
        this.InitializeComponent();
    }
}

