# Uno Platform University Mobile App

A full-featured Uno Platform application with MVVM architecture, SQLite database, and multi-platform support (Android & WebAssembly).

## 📋 Project Structure

```
Uno_Platform/
├── Models/              # Data models (Product)
├── ViewModels/          # MVVM ViewModels using CommunityToolkit.Mvvm
├── Views/               # XAML pages (Login, Home, ProductList, ProductDetail)
├── Services/            # Business logic services
│   ├── DatabaseService      # CRUD operations
│   ├── NavigationService   # Page navigation
│   ├── AuthenticationService # Login/logout
│   ├── ProductService      # Search & filter
│   ├── ToastService        # User notifications
│   ├── AppState           # Global state management
│   └── ServiceLocator     # Service access
├── Database/           # SQLite database context
│   ├── AppDbContext        # SQLite (Android)
│   └── InMemoryDbContext   # In-memory (WebAssembly)
├── Controls/           # Reusable UI controls
│   └── LoadingIndicator    # Loading overlay
├── Converters/         # Value converters for XAML bindings
├── Themes/            # Centralized styling
│   └── Theme.xaml         # Colors, fonts, styles
├── docs/              # Documentation
│   ├── Requirements.md
│   ├── UI_Wireframe.md
│   ├── UI_Design.md
│   ├── NavigationFlow.md
│   ├── UseCaseDiagram.md
│   ├── ClassDiagram.md
│   └── ReleaseBuild.md
└── release/           # Release build files
    ├── README.txt
    ├── keystore-placeholder.txt
    └── signing-template.json
```

## 🏗️ Architecture

### MVVM Pattern
- **Models**: Data entities (`Product`)
- **ViewModels**: Business logic and data binding (`LoginViewModel`, `HomeViewModel`, etc.)
- **Views**: XAML UI pages
- **Services**: Reusable business logic

### Services
- **DatabaseService**: CRUD operations for products
- **AuthenticationService**: Login, logout, session management
- **ProductService**: Search and filter products
- **NavigationService**: Page navigation using Frame
- **ToastService**: User notifications
- **AppState**: Global application state management

### Database
- **Android**: SQLite using `SQLitePCLRaw.bundle_green`
- **WebAssembly**: In-memory storage (`InMemoryDbContext`)
- **AppDbContext**: Manages database connection and table creation (Android)
- **DatabaseService**: Provides CRUD operations with error handling

### Navigation
- **NavigationService**: Handles page navigation using Frame
- Navigation flow: Login → Home → ProductList → ProductDetail
- Back stack management
- Parameter passing between pages

## 🚀 Running the Application

### Prerequisites
- .NET 9.0 SDK
- Uno Platform workload installed
- For Android: Android SDK and emulator
- For WebAssembly: `wasm-tools` workload

### Install Required Workloads

```powershell
dotnet workload install wasm-tools
```

### Run on WebAssembly

```powershell
cd Uno_Platform\Uno_Platform
dotnet run --framework net9.0-browserwasm
```

The application will start on `http://localhost:5000` (or another port if 5000 is busy).

### Run on Android

1. Start an Android emulator or connect a device
2. Run:

```powershell
cd Uno_Platform\Uno_Platform
dotnet run --framework net9.0-android
```

## 📱 Application Features

### 1. Login Page
- Simple authentication (username/password validation)
- Minimum 3 characters for both fields
- Input validation with error messages
- Loading indicator during login
- Navigates to HomePage on successful login
- Session management via AppState

### 2. Home Page
- Personalized welcome message with username
- Navigation to Product List
- Logout functionality with session clearing

### 3. Product List Page
- Displays all products from database
- **Search functionality**: Search by keyword (name or description)
- **Price filtering**: Filter by min/max price range
- Shows product image (emoji), name, price, and description
- Click on product to view details
- Refresh button to reload products
- Loading indicator during data operations
- Empty state message when no products found

### 4. Product Detail Page
- View product details
- Edit mode to update product information
- Input validation for product fields
- Delete product functionality
- Loading indicator during save/delete
- Error handling with user feedback
- Back navigation

## 🗄️ Database

### Product Model
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }  // Emoji or image path
}
```

### DatabaseService CRUD Operations
- `GetAllProducts()` - Retrieve all products
- `GetProductById(int id)` - Get single product
- `AddProduct(Product product)` - Add new product
- `UpdateProduct(Product product)` - Update existing product
- `DeleteProduct(int id)` - Delete product
- `SeedSampleData()` - Initialize with sample data
- **Error Handling**: All operations include try-catch with user-friendly error messages

### ProductService Operations
- `SearchProducts(string keyword)` - Search by name or description
- `FilterByPrice(decimal? min, decimal? max)` - Filter by price range
- `ValidateProduct(Product product)` - Input validation

### Sample Data
The app automatically seeds 5 sample products on first launch:
- Laptop 💻 ($999.99)
- Smartphone 📱 ($699.99)
- Headphones 🎧 ($199.99)
- Tablet 📱 ($499.99)
- Smartwatch ⌚ ($299.99)

## 🔧 Dependencies

- **Uno.Sdk**: Uno Platform SDK 6.4.24
- **CommunityToolkit.Mvvm**: MVVM helpers 8.4.0
- **SQLitePCLRaw.bundle_green**: SQLite database support 2.1.7 (Android only)

## 🎨 UI/UX Features

### Theme System
- Centralized `Theme.xaml` with colors, fonts, spacing
- Reusable button styles (Primary, Secondary, Danger)
- Standard text box styles
- Consistent spacing and border radius

### Loading Indicators
- Overlay loading indicator component
- Shows during async operations
- Customizable messages

### Toast Notifications
- Success, error, and info messages
- Platform-independent implementation
- Debug output for development

### Input Validation
- Login form validation
- Product creation/editing validation
- Real-time error messages
- User-friendly feedback

## 📝 Notes

- Database file location:
  - **WebAssembly**: IndexedDB (browser)
  - **Android**: Local app data folder
- Login credentials: Any username/password with at least 3 characters
- All pages use clean, simple UI with proper spacing and colors

## 🐛 Troubleshooting

### Build Errors
- Ensure all workloads are installed: `dotnet workload install wasm-tools`
- Clean and rebuild: `dotnet clean && dotnet build`

### Port Already in Use
- Kill existing processes: `taskkill /F /IM dotnet.exe`
- Or use a different port in launch settings

### Android Emulator Not Starting
- Ensure Android SDK is properly installed
- Check emulator is running before building

## 📚 Documentation

All documentation is available in the `/docs` folder:

- **Requirements.md**: Functional and non-functional requirements
- **UI_Wireframe.md**: Wireframe layouts for all screens
- **UI_Design.md**: Visual design specifications
- **NavigationFlow.md**: Navigation structure and flow
- **UseCaseDiagram.md**: Use cases and user stories
- **ClassDiagram.md**: Class structure and relationships
- **ReleaseBuild.md**: Instructions for building release versions

## 🔐 Authentication

### AuthenticationService
- `Login(username, password)` - Validates and authenticates user
- `Logout()` - Clears session
- `CheckLocalSession()` - Checks if user is authenticated
- `IsAuthenticated` - Current authentication status
- `CurrentUser` - Currently logged-in username

### AppState
- Singleton pattern for global state
- Tracks authentication status
- Notifies state changes via events

## 🔍 Search & Filter

### Search Products
- Case-insensitive keyword search
- Searches in product name and description
- Real-time filtering

### Filter by Price
- Filter by minimum price
- Filter by maximum price
- Filter by price range
- Clear filter to show all products

## 📦 Release Build

See `/docs/ReleaseBuild.md` for detailed instructions on:
- Building Android APK/AAB
- Configuring signing
- Building WebAssembly release
- Store submission process

## 📚 Additional Resources

- [Uno Platform Documentation](https://platform.uno/docs/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- [SQLite with .NET](https://www.sqlite.org/index.html)

