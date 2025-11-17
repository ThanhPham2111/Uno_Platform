# UI Design Documentation

## Overview
This document describes the visual design specifications for all screens in the Uno Platform University Mobile App.

## Design System

### Colors
```xml
Primary Blue: #0078D4
Secondary Blue: #E8F4F8
Background: #FFFFFF
Surface: #F3F3F3
Error: #FF0000
Success: #00AA00
Text Primary: #000000
Text Secondary: #666666
Border: #CCCCCC
```

### Typography
- **Headings:** Segoe UI, 24-32px, Bold
- **Subheadings:** Segoe UI, 18-20px, SemiBold
- **Body:** Segoe UI, 16px, Regular
- **Labels:** Segoe UI, 14px, SemiBold
- **Small Text:** Segoe UI, 12px, Regular

### Spacing Scale
- **XS:** 5px
- **S:** 10px
- **M:** 20px
- **L:** 30px
- **XL:** 40px

### Border Radius
- **Small:** 5px
- **Medium:** 10px
- **Large:** 15px

---

## Screen Designs

### 1. Login Page

**Visual Description:**
- Centered layout with white background
- Blue header bar at top (optional)
- Large "Login" title in bold
- Two input fields with labels above
- Full-width login button in primary blue
- Error message appears below inputs in red

**Screenshot Placeholder:**
```
[SCREENSHOT: login-page.png]
- Shows centered login form
- Username and password fields
- Blue login button
- Clean, minimal design
```

**Component Specifications:**
- **Title:** FontSize 32, FontWeight Bold, Center-aligned, Margin bottom 20
- **Input Fields:** Height 40, Border 1px solid #CCCCCC, BorderRadius 5px, Padding 10
- **Button:** Height 50, Background #0078D4, Foreground White, BorderRadius 5px
- **Error Text:** FontSize 14, Foreground Red, Margin top 10

---

### 2. Home Page

**Visual Description:**
- Centered welcome message
- Large "View Products" button in primary blue
- Smaller "Logout" button below
- Clean, spacious layout

**Screenshot Placeholder:**
```
[SCREENSHOT: home-page.png]
- Shows welcome message
- Two action buttons
- Simple, friendly design
```

**Component Specifications:**
- **Welcome Message:** FontSize 28, FontWeight SemiBold, Center-aligned, Margin bottom 20
- **View Products Button:** Height 60, FontSize 18, Background #0078D4, Foreground White
- **Logout Button:** Height 50, Background Transparent, Border 1px solid #0078D4, Foreground #0078D4

---

### 3. Product List Page

**Visual Description:**
- Blue header bar with "Products" title and refresh button
- Search bar below header
- Price filter controls (min/max inputs)
- Scrollable list of product cards
- Each card shows emoji, name, price, and description

**Screenshot Placeholder:**
```
[SCREENSHOT: product-list-page.png]
- Shows header with title and refresh
- Search bar visible
- Multiple product cards in list
- Clean card design with emoji icons
```

**Component Specifications:**
- **Header:** Background #0078D4, Padding 20, Height Auto
- **Product Card:** Background #F3F3F3, Padding 20, BorderRadius 10, Margin bottom 15
- **Product Image:** FontSize 48, Margin right 15
- **Product Name:** FontSize 20, FontWeight SemiBold, Color Black
- **Product Price:** FontSize 16, Color #0078D4, FontWeight SemiBold
- **Product Description:** FontSize 14, Color Gray, TextWrapping Wrap

---

### 4. Product Detail Page

**Visual Description:**
- Large product emoji/icon at top (centered)
- Edit/Cancel button in top right
- Form fields in view mode (read-only) or edit mode (editable)
- Action buttons at bottom: Save, Delete, Back

**Screenshot Placeholder:**
```
[SCREENSHOT: product-detail-page.png]
- Shows large product icon
- Product information fields
- Edit mode toggle
- Action buttons at bottom
```

**Component Specifications:**
- **Product Icon:** FontSize 120, Center-aligned, Margin top/bottom 20
- **Edit Button:** Position Right, Margin bottom 10
- **Input Fields:** Height 40, Border 1px solid #CCCCCC when editable
- **Display Fields:** FontSize 24 (name), 20 (price), 16 (description)
- **Action Buttons:** Height 50, Horizontal layout, Spacing 10

---

## Interactive States

### Buttons
- **Default:** Background #0078D4, Foreground White
- **Hover:** Background #0066BB (darker blue)
- **Pressed:** Background #0055AA (even darker)
- **Disabled:** Background #CCCCCC, Foreground #666666

### Input Fields
- **Default:** Border #CCCCCC, Background White
- **Focused:** Border #0078D4, Background White
- **Error:** Border #FF0000, Background #FFF5F5
- **Disabled:** Background #F3F3F3, Foreground #666666

### Cards
- **Default:** Background #F3F3F3, Shadow None
- **Hover:** Shadow 0 2px 4px rgba(0,0,0,0.1)
- **Pressed:** Shadow 0 1px 2px rgba(0,0,0,0.1)

---

## Loading States

### Loading Indicator
- Circular progress indicator
- Center-aligned
- Color: #0078D4
- Size: 40x40 pixels
- Appears during data operations

### Skeleton Screens
- Gray placeholder boxes
- Animated shimmer effect
- Matches content layout

---

## Error States

### Error Messages
- Red text (#FF0000)
- FontSize 14
- Appears below relevant input
- Clear, actionable message

### Toast Notifications
- Background: Dark gray (#333333)
- Foreground: White
- BorderRadius: 5px
- Padding: 15px
- Position: Bottom center
- Auto-dismiss after 3 seconds

---

## Success States

### Success Messages
- Green text (#00AA00)
- FontSize 14
- Appears below action button
- Confirmation message

---

## Responsive Design

### Breakpoints
- **Small:** < 600px width
- **Medium:** 600px - 900px width
- **Large:** > 900px width

### Adaptations
- **Small:** Single column, stacked buttons
- **Medium:** Two columns where appropriate
- **Large:** Three columns, wider spacing

---

## Accessibility

### Contrast Ratios
- Text on background: Minimum 4.5:1
- Large text: Minimum 3:1
- Interactive elements: Minimum 3:1

### Touch Targets
- Minimum size: 44x44 pixels
- Adequate spacing between targets

### Screen Reader Support
- All interactive elements have labels
- Form fields have associated labels
- Buttons have descriptive text

---

## Animation Guidelines

### Transitions
- **Page Navigation:** 300ms fade/slide
- **Button Press:** 100ms scale down
- **Modal Open:** 250ms fade in
- **Toast Show:** 200ms slide up

### Easing
- **Standard:** EaseInOut
- **Enter:** EaseOut
- **Exit:** EaseIn

---

## Design Tokens

All design values are centralized in `Themes/Theme.xaml`:
- Colors
- Font sizes
- Spacing values
- Border radius
- Shadows
- Animations

