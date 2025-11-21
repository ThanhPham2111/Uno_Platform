using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Uno_Platform.Services;

namespace Uno_Platform.ViewModels;

public partial class OrderSuccessViewModel : ObservableObject
{
    [RelayCommand]
    private void ContinueShopping()
    {
        // Navigate back to Product List and clear back stack to prevent going back to success page
        var pageType = typeof(Uno_Platform.Views.ProductListPage);
        ServiceLocator.NavigationService.NavigateTo(pageType);
        
        // Optional: Clear back stack if supported by NavigationService, 
        // or just navigating to ProductList is enough as it's usually the root.
        // For now, simple navigation is sufficient.
    }
}
