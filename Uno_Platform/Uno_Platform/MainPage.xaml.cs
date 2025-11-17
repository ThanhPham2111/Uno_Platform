namespace Uno_Platform;

public sealed partial class MainPage : Page
{
    private int _counter = 0;
    private int _clickCount = 0;

    public MainPage()
    {
        this.InitializeComponent();
        UpdateCounter();
    }

    private void OnIncrementClick(object sender, RoutedEventArgs e)
    {
        _counter++;
        _clickCount++;
        UpdateCounter();
    }

    private void OnDecrementClick(object sender, RoutedEventArgs e)
    {
        _counter--;
        _clickCount++;
        UpdateCounter();
    }

    private void OnResetClick(object sender, RoutedEventArgs e)
    {
        _counter = 0;
        _clickCount++;
        UpdateCounter();
    }

    private void OnColorClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string colorName)
        {
            var color = colorName switch
            {
                "Red" => Windows.UI.Color.FromArgb(255, 255, 200, 200),
                "Green" => Windows.UI.Color.FromArgb(255, 200, 255, 200),
                "Blue" => Windows.UI.Color.FromArgb(255, 200, 200, 255),
                "Yellow" => Windows.UI.Color.FromArgb(255, 255, 255, 200),
                _ => Windows.UI.Color.FromArgb(255, 255, 255, 255)
            };
            
            this.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(color);
            _clickCount++;
            UpdateCounter();
        }
    }

    private void UpdateCounter()
    {
        CounterText.Text = _counter.ToString();
        ClickCountText.Text = $"Số lần nhấn: {_clickCount}";
    }
}
