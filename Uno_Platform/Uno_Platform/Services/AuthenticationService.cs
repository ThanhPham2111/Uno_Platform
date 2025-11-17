using Uno_Platform.Services;

namespace Uno_Platform.Services;

public class AuthenticationService
{
    private readonly AppState _appState;

    public AuthenticationService()
    {
        _appState = AppState.Instance;
    }

    public bool Login(string username, string password)
    {
        // Validate input
        if (!ValidateLoginInput(username, password))
        {
            return false;
        }

        // Simple authentication - in production, use proper authentication
        // For demo: accept any username/password with at least 3 characters
        if (username.Length >= 3 && password.Length >= 3)
        {
            _appState.SetAuthenticated(username);
            return true;
        }

        return false;
    }

    public void Logout()
    {
        _appState.ClearAuthentication();
    }

    public bool CheckLocalSession()
    {
        return _appState.IsAuthenticated;
    }

    public bool IsAuthenticated => _appState.IsAuthenticated;

    public string? CurrentUser => _appState.CurrentUser;

    private bool ValidateLoginInput(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        if (username.Length < 3)
        {
            return false;
        }

        if (password.Length < 3)
        {
            return false;
        }

        return true;
    }
}

