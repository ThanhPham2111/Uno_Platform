# UI Wireframe Documentation

## Overview
This document describes the wireframe layouts for all screens in the Uno Platform University Mobile App.

## Screen 1: Login Page

```
┌─────────────────────────────────┐
│                                 │
│         [Logo/Title]            │
│                                 │
│      ┌─────────────────┐        │
│      │   Username      │        │
│      └─────────────────┘        │
│                                 │
│      ┌─────────────────┐        │
│      │   Password      │        │
│      └─────────────────┘        │
│                                 │
│      [Error Message]            │
│                                 │
│      ┌─────────────────┐        │
│      │     LOGIN       │        │
│      └─────────────────┘        │
│                                 │
└─────────────────────────────────┘
```

### Layout Hierarchy
- **Container:** ScrollViewer
  - **StackPanel** (Vertical, Centered, Spacing: 20)
    - **TextBlock** (Title: "Login", FontSize: 32, Bold)
    - **TextBlock** (Label: "Username")
    - **TextBox** (Username input, Height: 40)
    - **TextBlock** (Label: "Password")
    - **PasswordBox** (Password input, Height: 40)
    - **TextBlock** (Error message, Red, Collapsed by default)
    - **Button** (Login, Height: 50, Full width)

### Components
- TextBox for username
- PasswordBox for password
- Button for login action
- TextBlock for error messages

---

## Screen 2: Home Page

```
┌─────────────────────────────────┐
│                                 │
│    Welcome to Uno Platform      │
│         App!                    │
│                                 │
│      ┌─────────────────┐        │
│      │  View Products  │        │
│      └─────────────────┘        │
│                                 │
│      ┌─────────────────┐        │
│      │     Logout      │        │
│      └─────────────────┘        │
│                                 │
└─────────────────────────────────┘
```

### Layout Hierarchy
- **Container:** ScrollViewer
  - **StackPanel** (Vertical, Centered, Spacing: 30)
    - **TextBlock** (Welcome message, FontSize: 28, SemiBold)
    - **Button** (View Products, Height: 60, FontSize: 18)
    - **Button** (Logout, Height: 50)

### Components
- TextBlock for welcome message
- Button for navigation to products
- Button for logout

---

## Screen 3: Product List Page

```
┌─────────────────────────────────┐
│ ┌─────────────────────────────┐ │
│ │ Products        [Refresh]   │ │
│ └─────────────────────────────┘ │
│                                 │
│ ┌─────────────────────────────┐ │
│ │ 🔍 [Search...]              │ │
│ └─────────────────────────────┘ │
│                                 │
│ ┌─────────────────────────────┐ │
│ │ 💻  Laptop                  │ │
│ │     $999.99                 │ │
│ │     High-performance...     │ │
│ └─────────────────────────────┘ │
│                                 │
│ ┌─────────────────────────────┐ │
│ │ 📱  Smartphone              │ │
│ │     $699.99                 │ │
│ │     Latest smartphone...    │ │
│ └─────────────────────────────┘ │
│                                 │
│ [Scrollable List]               │
│                                 │
└─────────────────────────────────┘
```

### Layout Hierarchy
- **Grid** (2 rows)
  - **Row 0:** Header Border
    - **Grid** (2 columns)
      - **Column 0:** TextBlock (Title: "Products")
      - **Column 1:** Button (Refresh)
  - **Row 1:** ScrollViewer
    - **StackPanel** (Vertical, Spacing: 15)
      - **Border** (Search container)
        - **TextBox** (Search input)
      - **Border** (Filter container)
        - **StackPanel** (Horizontal)
          - **TextBlock** (Min Price label)
          - **TextBox** (Min price input)
          - **TextBlock** (Max Price label)
          - **TextBox** (Max price input)
          - **Button** (Apply Filter)
      - **ItemsControl** (Product list)
        - **DataTemplate:** Border → Button → StackPanel (Horizontal)
          - **TextBlock** (Product image/emoji)
          - **StackPanel** (Vertical)
            - **TextBlock** (Product name)
            - **TextBlock** (Price)
            - **TextBlock** (Description)

### Components
- TextBox for search
- TextBox for price filters
- Button for refresh
- Button for apply filter
- ItemsControl for product list
- Border for product cards

---

## Screen 4: Product Detail Page

```
┌─────────────────────────────────┐
│                                 │
│            💻                   │
│        (Large Icon)              │
│                                 │
│      [Edit] [Cancel]            │
│                                 │
│      Name:                      │
│      ┌─────────────────┐        │
│      │   Laptop        │        │
│      └─────────────────┘        │
│                                 │
│      Price:                     │
│      ┌─────────────────┐        │
│      │   $999.99       │        │
│      └─────────────────┘        │
│                                 │
│      Description:               │
│      ┌─────────────────┐        │
│      │ High-performance│        │
│      │ laptop for work  │        │
│      │ and gaming      │        │
│      └─────────────────┘        │
│                                 │
│      Image:                     │
│      ┌─────────────────┐        │
│      │       💻        │        │
│      └─────────────────┘        │
│                                 │
│  [Save] [Delete] [Back]        │
│                                 │
└─────────────────────────────────┘
```

### Layout Hierarchy
- **Container:** ScrollViewer
  - **StackPanel** (Vertical, Spacing: 20)
    - **TextBlock** (Product image/emoji, FontSize: 120, Centered)
    - **StackPanel** (Horizontal, Right-aligned)
      - **Button** (Edit/Cancel toggle)
    - **TextBlock** (Label: "Name")
    - **TextBox** (Name input, visible in edit mode)
    - **TextBlock** (Name display, visible in view mode)
    - **TextBlock** (Label: "Price")
    - **TextBox** (Price input, visible in edit mode)
    - **TextBlock** (Price display, visible in view mode)
    - **TextBlock** (Label: "Description")
    - **TextBox** (Description input, multiline, visible in edit mode)
    - **TextBlock** (Description display, visible in view mode)
    - **TextBlock** (Label: "Image")
    - **TextBox** (Image input, visible in edit mode)
    - **TextBlock** (Image display, visible in view mode)
    - **StackPanel** (Horizontal, Spacing: 10)
      - **Button** (Save, visible in edit mode)
      - **Button** (Delete)
      - **Button** (Back)

### Components
- TextBox for editable fields
- TextBlock for display fields
- Button for actions (Edit, Save, Delete, Back)
- Toggle between view and edit modes

---

## Design Principles

1. **Consistency:** All screens use the same color scheme and spacing
2. **Clarity:** Clear labels and instructions for all inputs
3. **Feedback:** Loading indicators and error messages for user actions
4. **Accessibility:** Adequate touch targets (minimum 44x44 pixels)
5. **Responsiveness:** Layouts adapt to different screen sizes

## Color Scheme
- Primary: #0078D4 (Blue)
- Background: #FFFFFF (White)
- Surface: #F3F3F3 (Light Gray)
- Text Primary: #000000 (Black)
- Text Secondary: #666666 (Gray)
- Error: #FF0000 (Red)
- Success: #00AA00 (Green)

## Typography
- Headings: 24-32px, Bold
- Body: 16px, Regular
- Labels: 14px, SemiBold
- Small text: 12px, Regular

## Spacing
- Small: 5px
- Medium: 10px
- Large: 20px
- Extra Large: 30px

