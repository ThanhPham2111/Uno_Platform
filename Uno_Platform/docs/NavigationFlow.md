# Navigation Flow Documentation

## Overview
This document describes the navigation structure and flow between screens in the Uno Platform University Mobile App.

## Navigation Architecture

The app uses a **Frame-based navigation** system provided by Uno Platform. The `NavigationService` manages all navigation operations.

## Navigation Graph

```
                    ┌─────────────┐
                    │   Login     │
                    │   Page      │
                    └──────┬──────┘
                           │
                           │ (Successful Login)
                           ▼
                    ┌─────────────┐
                    │    Home     │
                    │    Page     │
                    └──────┬──────┘
                           │
            ┌──────────────┼──────────────┐
            │                              │
            │ (View Products)              │ (Logout)
            ▼                              ▼
    ┌───────────────┐              ┌─────────────┐
    │ Product List  │              │   Login     │
    │     Page      │              │   Page      │
    └───────┬───────┘              └─────────────┘
            │
            │ (Select Product)
            ▼
    ┌───────────────┐
    │ Product Detail│
    │     Page      │
    └───────┬───────┘
            │
            │ (Back)
            ▼
    ┌───────────────┐
    │ Product List  │
    │     Page      │
    └───────────────┘
```

## Navigation Routes

### Route 1: Initial Launch → Login
- **Entry Point:** App.xaml.cs `OnLaunched`
- **Destination:** `Views.LoginPage`
- **Parameters:** None
- **Back Stack:** Empty

### Route 2: Login → Home
- **Trigger:** Successful login validation
- **Source:** `Views.LoginPage`
- **Destination:** `Views.HomePage`
- **Parameters:** None
- **Back Stack:** Contains LoginPage

### Route 3: Home → Product List
- **Trigger:** "View Products" button click
- **Source:** `Views.HomePage`
- **Destination:** `Views.ProductListPage`
- **Parameters:** None
- **Back Stack:** Contains LoginPage, HomePage

### Route 4: Product List → Product Detail
- **Trigger:** Product card click
- **Source:** `Views.ProductListPage`
- **Destination:** `Views.ProductDetailPage`
- **Parameters:** `Product` object
- **Back Stack:** Contains LoginPage, HomePage, ProductListPage

### Route 5: Product Detail → Product List (Back)
- **Trigger:** Back button or navigation back
- **Source:** `Views.ProductDetailPage`
- **Destination:** `Views.ProductListPage`
- **Parameters:** None
- **Back Stack:** Contains LoginPage, HomePage

### Route 6: Any Page → Login (Logout)
- **Trigger:** Logout button click
- **Source:** Any page
- **Destination:** `Views.LoginPage`
- **Parameters:** None
- **Back Stack:** Cleared (empty)

## Navigation Service Implementation

### Service Location
- **File:** `Services/NavigationService.cs`
- **Access:** Via `ServiceLocator.NavigationService`

### Methods

#### NavigateTo
```csharp
bool NavigateTo(Type pageType, object? parameter = null)
```
- Navigates to specified page type
- Optional parameter for passing data
- Returns true if navigation successful

#### GoBack
```csharp
void GoBack()
```
- Navigates to previous page in back stack
- Only works if `CanGoBack()` returns true

#### CanGoBack
```csharp
bool CanGoBack()
```
- Checks if back navigation is possible
- Returns true if back stack has entries

#### ClearBackStack
```csharp
void ClearBackStack()
```
- Removes all entries from back stack
- Used during logout

## Navigation Parameters

### Product Detail Page
**Parameter Type:** `Product` object

**Usage:**
```csharp
var product = new Product { Id = 1, Name = "Laptop", ... };
ServiceLocator.NavigationService.NavigateTo(typeof(ProductDetailPage), product);
```

**Receiving:**
```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    if (e.Parameter is Product product)
    {
        ViewModel.LoadProduct(product);
    }
}
```

## Back Button Behavior

### Android
- Hardware back button navigates back through stack
- At Login page, back button exits app

### WebAssembly
- Browser back button navigates through stack
- Can be intercepted if needed

## Deep Linking (Future Enhancement)

Currently not implemented, but structure supports:
- URL-based navigation
- Parameter passing via query strings
- State restoration

## Navigation Guards

### Authentication Guard
- **Location:** App.xaml.cs
- **Behavior:** Redirects to Login if not authenticated
- **Status:** Currently simple (always allows navigation)

### Data Validation Guard
- **Location:** ViewModels
- **Behavior:** Prevents navigation if data is invalid
- **Example:** ProductDetailViewModel validates before saving

## Navigation Events

### OnNavigatedTo
- Fired when page becomes active
- Used to load data, initialize view
- Example: `ProductListPage` loads products

### OnNavigatedFrom
- Fired when leaving page
- Used to save state, cleanup
- Example: Save draft data

## State Management During Navigation

### ViewModel State
- ViewModels are created per navigation
- State is not preserved between navigations
- Use AppState for persistent data

### Page State
- Pages are cached by Frame
- Can be reused if in back stack
- State preserved in ViewModel

## Navigation Best Practices

1. **Always use NavigationService:** Don't access Frame directly
2. **Clear back stack on logout:** Prevent unauthorized access
3. **Pass minimal data:** Use IDs instead of full objects when possible
4. **Handle navigation failures:** Check return value of NavigateTo
5. **Clean up on navigation:** Dispose resources in OnNavigatedFrom

## Error Handling

### Navigation Failures
- Logged to debug output
- User sees error message
- App remains on current page

### Missing Parameters
- Check for null in OnNavigatedTo
- Navigate back if required parameter missing
- Show appropriate error message

## Testing Navigation

### Test Cases
1. Navigate from Login to Home
2. Navigate from Home to Product List
3. Navigate from Product List to Product Detail
4. Navigate back from Product Detail
5. Logout clears back stack
6. Back button works correctly
7. Parameter passing works

### Test Scenarios
- **Scenario 1:** User logs in, views products, views detail, goes back
- **Scenario 2:** User logs in, views products, logs out, logs in again
- **Scenario 3:** User navigates deep, uses back button multiple times

