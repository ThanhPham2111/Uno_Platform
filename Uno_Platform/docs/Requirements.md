# Project Requirements Document

## 1. Project Overview

**Project Name:** Uno Platform University Mobile App  
**Platform:** Android & WebAssembly  
**Architecture:** MVVM (Model-View-ViewModel)  
**Database:** SQLite (Android) / In-Memory (WebAssembly)

## 2. Functional Requirements

### 2.1 Authentication Module
- **FR-001:** User must be able to login with username and password
- **FR-002:** Username and password must be at least 3 characters
- **FR-003:** System must validate credentials before allowing access
- **FR-004:** System must maintain session state after login
- **FR-005:** User must be able to logout from any screen

### 2.2 Product Management Module
- **FR-006:** System must display list of all products
- **FR-007:** User must be able to view product details
- **FR-008:** User must be able to search products by keyword
- **FR-009:** User must be able to filter products by price range
- **FR-010:** User must be able to edit product information
- **FR-011:** User must be able to delete products
- **FR-012:** System must validate product data before saving

### 2.3 Navigation Module
- **FR-013:** Navigation flow: Login → Home → Product List → Product Detail
- **FR-014:** User must be able to navigate back from any screen
- **FR-015:** Back stack must be cleared on logout

### 2.4 Data Persistence Module
- **FR-016:** System must persist products in local database (Android)
- **FR-017:** System must use in-memory storage for WebAssembly
- **FR-018:** System must seed sample data on first launch

## 3. Non-Functional Requirements

### 3.1 Performance
- **NFR-001:** App must load within 3 seconds
- **NFR-002:** Database operations must complete within 1 second
- **NFR-003:** UI must remain responsive during data operations

### 3.2 Usability
- **NFR-004:** All screens must have consistent UI/UX
- **NFR-005:** Error messages must be clear and actionable
- **NFR-006:** Loading indicators must be shown during async operations

### 3.3 Reliability
- **NFR-007:** App must handle network errors gracefully
- **NFR-008:** Database errors must not crash the application
- **NFR-009:** Input validation must prevent invalid data entry

### 3.4 Maintainability
- **NFR-010:** Code must follow MVVM architecture
- **NFR-011:** Services must be testable and reusable
- **NFR-012:** Code must be well-documented

## 4. Technical Requirements

### 4.1 Platform Support
- **TR-001:** App must run on Android 8.0 (API 26) and above
- **TR-002:** App must run on modern browsers (Chrome, Firefox, Edge, Safari)

### 4.2 Dependencies
- **TR-003:** Uno Platform SDK 6.4.24
- **TR-004:** CommunityToolkit.Mvvm 8.4.0
- **TR-005:** SQLitePCLRaw.bundle_green 2.1.7 (Android only)

### 4.3 Database Schema
- **TR-006:** Product table with fields: Id, Name, Price, Description, Image

## 5. User Stories

### US-001: Login
**As a** user  
**I want to** login to the application  
**So that** I can access product management features

**Acceptance Criteria:**
- User can enter username and password
- System validates input (min 3 characters)
- System navigates to Home page on successful login
- System shows error message on invalid credentials

### US-002: View Products
**As a** user  
**I want to** view a list of all products  
**So that** I can see available items

**Acceptance Criteria:**
- Product list displays name, price, description, and image
- List is scrollable
- Loading indicator shows while data loads
- Empty state message shows when no products exist

### US-003: Search Products
**As a** user  
**I want to** search products by keyword  
**So that** I can quickly find specific items

**Acceptance Criteria:**
- Search input is available on product list page
- Results update as user types
- Search is case-insensitive
- Search matches product name and description

### US-004: Filter Products
**As a** user  
**I want to** filter products by price range  
**So that** I can find products within my budget

**Acceptance Criteria:**
- Filter controls for min and max price
- Results update when filter is applied
- Filter can be cleared to show all products

### US-005: Edit Product
**As a** user  
**I want to** edit product information  
**So that** I can update product details

**Acceptance Criteria:**
- Edit mode can be toggled on product detail page
- All fields are editable in edit mode
- Changes are validated before saving
- Success message shown after save

## 6. Constraints

- **C-001:** WebAssembly does not support SQLite, must use in-memory storage
- **C-002:** Android requires minimum API level 26
- **C-003:** Project must use Uno Platform single-project structure

## 7. Assumptions

- **A-001:** Users have basic understanding of mobile applications
- **A-002:** Internet connection is not required (offline-first)
- **A-003:** Sample data is sufficient for demonstration purposes

## 8. Out of Scope

- User registration
- Product images (using emoji placeholders)
- Online synchronization
- Multi-user support
- Product categories
- Shopping cart functionality

