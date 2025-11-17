namespace Uno_Platform.Services;

public class AppState
{
    private static AppState? _instance;
    private bool _isAuthenticated;
    private string? _currentUser;

    public static AppState Instance
    {
        get
        {
            _instance ??= new AppState();
            return _instance;
        }
    }

    public bool IsAuthenticated
    {
        get => _isAuthenticated;
        private set
        {
            if (_isAuthenticated != value)
            {
                _isAuthenticated = value;
                StateChanged?.Invoke();
            }
        }
    }

    public string? CurrentUser
    {
        get => _currentUser;
        private set
        {
            if (_currentUser != value)
            {
                _currentUser = value;
                StateChanged?.Invoke();
            }
        }
    }

    public event Action? StateChanged;

    public void SetAuthenticated(string username)
    {
        CurrentUser = username;
        IsAuthenticated = true;
    }

    public void ClearAuthentication()
    {
        CurrentUser = null;
        IsAuthenticated = false;
    }
}

