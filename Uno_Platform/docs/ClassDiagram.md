# Class Diagram Documentation

## ASCII Class Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                         App                                  │
├─────────────────────────────────────────────────────────────┤
│ - MainWindow: Window                                         │
│ + App()                                                      │
│ + OnLaunched(args)                                           │
│ - InitializeDatabase()                                       │
└────────────┬────────────────────────────────────────────────┘
             │
             │ uses
             ▼
┌─────────────────────────────────────────────────────────────┐
│                    ServiceLocator                           │
├─────────────────────────────────────────────────────────────┤
│ + NavigationService: NavigationService                       │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                  NavigationService                           │
├─────────────────────────────────────────────────────────────┤
│ - _frame: Frame                                              │
│ + Initialize(frame: Frame)                                  │
│ + NavigateTo(pageType: Type, parameter: object): bool       │
│ + GoBack()                                                   │
│ + CanGoBack(): bool                                          │
│ + ClearBackStack()                                           │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    DatabaseService                           │
├─────────────────────────────────────────────────────────────┤
│ - _dbContext: AppDbContext / InMemoryDbContext               │
│ + GetAllProducts(): List<Product>                            │
│ + GetProductById(id: int): Product?                          │
│ + AddProduct(product: Product): bool                         │
│ + UpdateProduct(product: Product): bool                     │
│ + DeleteProduct(id: int): bool                               │
│ + SeedSampleData()                                           │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    AuthenticationService                     │
├─────────────────────────────────────────────────────────────┤
│ - _appState: AppState                                        │
│ + Login(username: string, password: string): bool            │
│ + Logout()                                                   │
│ + CheckLocalSession(): bool                                  │
│ + IsAuthenticated(): bool                                    │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                      ProductService                          │
├─────────────────────────────────────────────────────────────┤
│ - _databaseService: DatabaseService                          │
│ + SearchProducts(keyword: string): List<Product>             │
│ + FilterByPrice(min: decimal, max: decimal): List<Product>   │
│ + GetAllProducts(): List<Product>                            │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                      AppState                                │
├─────────────────────────────────────────────────────────────┤
│ + IsAuthenticated: bool                                      │
│ + CurrentUser: string?                                       │
│ + EventHandler StateChanged                                  │
│ + SetAuthenticated(username: string)                        │
│ + ClearAuthentication()                                      │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    AppDbContext                              │
├─────────────────────────────────────────────────────────────┤
│ - _connection: SQLiteConnection                              │
│ + Connection: SQLiteConnection                               │
│ - InitializeDatabase()                                       │
│ - GetDatabasePath(): string                                  │
│ - CreateTables()                                             │
│ + CloseConnection()                                          │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                  InMemoryDbContext                           │
├─────────────────────────────────────────────────────────────┤
│ - _products: List<Product>                                   │
│ - _nextId: int                                               │
│ + Products: List<Product>                                     │
│ + AddProduct(product: Product)                               │
│ + UpdateProduct(product: Product)                            │
│ + DeleteProduct(id: int)                                      │
│ + GetProductById(id: int): Product?                          │
│ + GetAllProducts(): List<Product>                            │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                        Product                               │
├─────────────────────────────────────────────────────────────┤
│ + Id: int                                                     │
│ + Name: string                                               │
│ + Price: decimal                                             │
│ + Description: string                                        │
│ + Image: string                                              │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    LoginViewModel                            │
├─────────────────────────────────────────────────────────────┤
│ + Username: string                                           │
│ + Password: string                                           │
│ + ErrorMessage: string                                       │
│ + LoginCommand: IRelayCommand                                │
│ - Login()                                                    │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    HomeViewModel                             │
├─────────────────────────────────────────────────────────────┤
│ + WelcomeMessage: string                                      │
│ + NavigateToProductsCommand: IRelayCommand                  │
│ + LogoutCommand: IRelayCommand                               │
│ - NavigateToProducts()                                       │
│ - Logout()                                                   │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                 ProductListViewModel                         │
├─────────────────────────────────────────────────────────────┤
│ - _databaseService: DatabaseService                          │
│ - _productService: ProductService                            │
│ + Products: List<Product>                                     │
│ + IsLoading: bool                                            │
│ + SearchKeyword: string                                      │
│ + MinPrice: decimal?                                         │
│ + MaxPrice: decimal?                                         │
│ + LoadProducts()                                             │
│ + NavigateToProductDetailCommand: IRelayCommand              │
│ + RefreshProductsCommand: IRelayCommand                       │
│ + SearchCommand: IRelayCommand                               │
│ + FilterCommand: IRelayCommand                               │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                ProductDetailViewModel                        │
├─────────────────────────────────────────────────────────────┤
│ - _databaseService: DatabaseService                          │
│ + Product: Product?                                          │
│ + IsEditMode: bool                                           │
│ + Name: string                                               │
│ + Price: decimal                                             │
│ + Description: string                                        │
│ + Image: string                                              │
│ + LoadProduct(product: Product)                              │
│ + ToggleEditModeCommand: IRelayCommand                       │
│ + SaveProductCommand: IRelayCommand                          │
│ + DeleteProductCommand: IRelayCommand                        │
│ + GoBackCommand: IRelayCommand                               │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                      ToastService                            │
├─────────────────────────────────────────────────────────────┤
│ + ShowMessage(message: string, duration: int)               │
│ + ShowError(message: string)                                 │
│ + ShowSuccess(message: string)                              │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    LoadingIndicator                          │
├─────────────────────────────────────────────────────────────┤
│ + IsLoading: bool                                            │
│ + Message: string                                            │
└─────────────────────────────────────────────────────────────┘
```

## Class Descriptions

### App
**Purpose:** Application entry point and initialization  
**Responsibilities:**
- Initialize SQLite (Android)
- Initialize database with sample data
- Set up navigation service
- Navigate to initial page (Login)

### ServiceLocator
**Purpose:** Provides global access to services  
**Pattern:** Singleton  
**Services:**
- NavigationService

### NavigationService
**Purpose:** Manages page navigation  
**Responsibilities:**
- Navigate between pages
- Manage back stack
- Handle navigation parameters

### DatabaseService
**Purpose:** Provides database operations  
**Responsibilities:**
- CRUD operations for products
- Seed sample data
- Handle platform-specific implementations

### AuthenticationService
**Purpose:** Manages user authentication  
**Responsibilities:**
- Validate login credentials
- Manage session state
- Check authentication status

### ProductService
**Purpose:** Provides product-related business logic  
**Responsibilities:**
- Search products
- Filter products by price
- Delegate to DatabaseService for data access

### AppState
**Purpose:** Manages global application state  
**Responsibilities:**
- Track authentication status
- Store current user
- Notify state changes

### AppDbContext
**Purpose:** SQLite database context (Android)  
**Responsibilities:**
- Manage database connection
- Create tables
- Provide connection to DatabaseService

### InMemoryDbContext
**Purpose:** In-memory storage (WebAssembly)  
**Responsibilities:**
- Store products in memory
- Provide CRUD operations
- Manage product IDs

### Product
**Purpose:** Data model for products  
**Properties:**
- Id: Unique identifier
- Name: Product name
- Price: Product price
- Description: Product description
- Image: Product image (emoji or path)

### ViewModels
**Purpose:** Business logic and data binding  
**Pattern:** MVVM with CommunityToolkit.Mvvm  
**Responsibilities:**
- Handle user interactions
- Manage view state
- Coordinate with services

### ToastService
**Purpose:** Display user notifications  
**Responsibilities:**
- Show temporary messages
- Display errors
- Show success messages

### LoadingIndicator
**Purpose:** Visual feedback during operations  
**Responsibilities:**
- Display loading state
- Show progress messages

## Relationships

### Composition
- App contains NavigationService (via ServiceLocator)
- DatabaseService contains AppDbContext/InMemoryDbContext
- ViewModels contain Services

### Dependency
- ViewModels depend on Services
- Services depend on Models
- Services depend on AppState

### Association
- NavigationService uses Frame
- DatabaseService uses Product model
- ViewModels use Product model

## Design Patterns

### MVVM (Model-View-ViewModel)
- Models: Product
- Views: XAML Pages
- ViewModels: LoginViewModel, HomeViewModel, etc.

### Singleton
- ServiceLocator
- AppState

### Repository
- DatabaseService acts as repository for Product

### Service Locator
- ServiceLocator provides global service access

## Platform-Specific Implementations

### Android
- Uses AppDbContext with SQLite
- Persistent storage

### WebAssembly
- Uses InMemoryDbContext
- In-memory storage only

## Data Flow

1. **User Action** → View (XAML)
2. **View** → ViewModel (Command)
3. **ViewModel** → Service (Method call)
4. **Service** → DatabaseService (Data access)
5. **DatabaseService** → Database (CRUD)
6. **Database** → DatabaseService (Result)
7. **DatabaseService** → Service (Data)
8. **Service** → ViewModel (Result)
9. **ViewModel** → View (Property update)
10. **View** → User (UI update)

