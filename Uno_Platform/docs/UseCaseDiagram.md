# Use Case Diagram

## ASCII Use Case Diagram

```
                    ┌─────────────────────┐
                    │   Mobile App User   │
                    └──────────┬──────────┘
                               │
                               │
        ┌──────────────────────┼──────────────────────┐
        │                      │                      │
        ▼                      ▼                      ▼
┌───────────────┐      ┌───────────────┐      ┌───────────────┐
│  Authentication│      │ Product      │      │  Navigation   │
│    System      │      │ Management   │      │    System     │
└───────┬───────┘      └───────┬───────┘      └───────┬───────┘
        │                      │                      │
        │                      │                      │
   ┌────┴────┐           ┌─────┴─────┐          ┌────┴────┐
   │         │           │           │          │         │
   ▼         ▼           ▼           ▼          ▼         ▼
┌─────┐  ┌─────┐    ┌────────┐  ┌────────┐  ┌─────┐  ┌─────┐
│Login│  │Logout│    │View    │  │Edit    │  │View │  │Back │
│     │  │     │    │Products│  │Product │  │Home │  │     │
└─────┘  └─────┘    └────────┘  └────────┘  └─────┘  └─────┘
                           │           │
                           ▼           ▼
                      ┌────────┐  ┌────────┐
                      │Search  │  │Delete  │
                      │Products│  │Product │
                      └────────┘  └────────┘
                           │
                           ▼
                      ┌────────┐
                      │Filter  │
                      │Products│
                      └────────┘
```

## Use Cases

### UC-001: Login
**Actor:** User  
**Precondition:** App is launched  
**Main Flow:**
1. User opens app
2. System displays login page
3. User enters username and password
4. User clicks Login button
5. System validates credentials
6. System navigates to Home page

**Alternative Flow:**
- 5a. Validation fails
  - 5a.1. System displays error message
  - 5a.2. User can retry login

**Postcondition:** User is logged in and on Home page

---

### UC-002: Logout
**Actor:** User  
**Precondition:** User is logged in  
**Main Flow:**
1. User clicks Logout button
2. System clears session
3. System clears navigation back stack
4. System navigates to Login page

**Postcondition:** User is logged out and on Login page

---

### UC-003: View Products
**Actor:** User  
**Precondition:** User is logged in  
**Main Flow:**
1. User navigates to Product List page
2. System displays loading indicator
3. System loads products from database
4. System displays product list
5. User can scroll through products

**Alternative Flow:**
- 4a. No products found
  - 4a.1. System displays empty state message

**Postcondition:** User sees list of products

---

### UC-004: View Product Details
**Actor:** User  
**Precondition:** User is on Product List page  
**Main Flow:**
1. User clicks on a product
2. System navigates to Product Detail page
3. System loads product information
4. System displays product details

**Postcondition:** User sees product details

---

### UC-005: Edit Product
**Actor:** User  
**Precondition:** User is on Product Detail page  
**Main Flow:**
1. User clicks Edit button
2. System switches to edit mode
3. User modifies product fields
4. User clicks Save button
5. System validates input
6. System saves changes to database
7. System displays success message
8. System switches back to view mode

**Alternative Flow:**
- 5a. Validation fails
  - 5a.1. System displays error message
  - 5a.2. User can correct and retry

**Postcondition:** Product is updated in database

---

### UC-006: Delete Product
**Actor:** User  
**Precondition:** User is on Product Detail page  
**Main Flow:**
1. User clicks Delete button
2. System confirms deletion
3. System deletes product from database
4. System navigates back to Product List
5. System refreshes product list

**Alternative Flow:**
- 2a. User cancels deletion
  - 2a.1. System remains on Product Detail page

**Postcondition:** Product is removed from database

---

### UC-007: Search Products
**Actor:** User  
**Precondition:** User is on Product List page  
**Main Flow:**
1. User enters search keyword
2. System filters products in real-time
3. System displays matching products
4. User can clear search to show all products

**Postcondition:** User sees filtered product list

---

### UC-008: Filter Products by Price
**Actor:** User  
**Precondition:** User is on Product List page  
**Main Flow:**
1. User enters minimum price
2. User enters maximum price
3. User clicks Apply Filter button
4. System filters products by price range
5. System displays filtered products
6. User can clear filter to show all products

**Postcondition:** User sees products within price range

---

### UC-009: Navigate Back
**Actor:** User  
**Precondition:** User is on any page except Login  
**Main Flow:**
1. User clicks Back button
2. System navigates to previous page
3. System restores previous page state

**Alternative Flow:**
- 1a. User is on Login page
  - 1a.1. System exits app (Android) or does nothing (WebAssembly)

**Postcondition:** User is on previous page

---

## Use Case Relationships

### Include Relationships
- **View Products** includes **Load Products from Database**
- **Edit Product** includes **Validate Product Data**
- **Delete Product** includes **Confirm Deletion**

### Extend Relationships
- **View Products** extends **Show Loading Indicator**
- **Edit Product** extends **Show Success Message**
- **Delete Product** extends **Refresh Product List**

## Actor Descriptions

### User
- **Type:** Primary Actor
- **Description:** End user of the mobile application
- **Goals:** Manage products, view information
- **Characteristics:** Basic mobile app knowledge

## System Boundaries

### In Scope
- Authentication
- Product CRUD operations
- Search and filter
- Navigation

### Out of Scope
- User registration
- Online synchronization
- Multi-user support
- Product categories
- Image upload

## Use Case Priorities

### High Priority
- UC-001: Login
- UC-003: View Products
- UC-004: View Product Details
- UC-005: Edit Product

### Medium Priority
- UC-002: Logout
- UC-006: Delete Product
- UC-007: Search Products
- UC-009: Navigate Back

### Low Priority
- UC-008: Filter Products by Price

