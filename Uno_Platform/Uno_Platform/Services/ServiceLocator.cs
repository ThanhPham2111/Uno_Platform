namespace Uno_Platform.Services;

public static class ServiceLocator
{
    public static NavigationService NavigationService { get; } = new NavigationService();
}

