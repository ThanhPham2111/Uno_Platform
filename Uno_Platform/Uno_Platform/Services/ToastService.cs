namespace Uno_Platform.Services;

public class ToastService
{
    private static ToastService? _instance;

    public static ToastService Instance
    {
        get
        {
            _instance ??= new ToastService();
            return _instance;
        }
    }

    public void ShowMessage(string message, int duration = 3000)
    {
        ShowToast(message, duration);
    }

    public void ShowError(string message)
    {
        ShowToast($"❌ {message}", 4000);
    }

    public void ShowSuccess(string message)
    {
        ShowToast($"✅ {message}", 3000);
    }

    private void ShowToast(string message, int duration)
    {
        try
        {
            // For now, use debug output
            // In a production app, you could use a proper toast notification system
            System.Diagnostics.Debug.WriteLine($"TOAST: {message}");
            
            // Try to show in UI if possible
            _ = Task.Run(async () =>
            {
                await Task.Delay(100);
                // Could implement a proper toast UI component here
                // For now, just log to debug
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error showing toast: {ex.Message}");
        }
    }
}

