# Hướng dẫn cấu hình API

## Cấu hình API Base URL

Ứng dụng cần kết nối với backend ASP.NET MVC để:
- Lấy danh sách sản phẩm (GET `/api/products`)
- Gửi đơn hàng (POST `/api/orders`)

### Cách cấu hình

1. Mở file `Uno_Platform/Services/ApiService.cs`
2. Tìm dòng:
   ```csharp
   _baseUrl = "https://localhost:5001/api"; // Thay đổi theo URL của ASP.NET MVC backend
   ```
3. Thay đổi URL thành địa chỉ backend của bạn:
   - Development: `http://localhost:5000/api` hoặc `https://localhost:5001/api`
   - Production: `https://your-domain.com/api`

### Ví dụ cấu hình

```csharp
// Development
_baseUrl = "http://localhost:5000/api";

// Production
_baseUrl = "https://api.yourstore.com/api";
```

## Yêu cầu API Backend

### 1. GET `/api/products`

**Request:**
```
GET /api/products
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "price": 999.99,
    "description": "High performance laptop",
    "image": "Assets/img/caby.png",
    "category": "Electronics"
  }
]
```

### 2. POST `/api/orders`

**Request:**
```
POST /api/orders
Content-Type: application/json
```

**Body:**
```json
{
  "customerName": "Nguyễn Văn A",
  "customerAddress": "123 Đường ABC, Quận 1, TP.HCM",
  "customerPhone": "0123456789",
  "totalPrice": 1999.98,
  "items": [
    {
      "productId": 1,
      "productName": "Laptop",
      "productPrice": 999.99,
      "quantity": 2,
      "totalPrice": 1999.98
    }
  ]
}
```

**Response:**
```
200 OK (hoặc 201 Created)
```

## Fallback khi API không khả dụng

Nếu API không khả dụng hoặc không kết nối được:
- Sản phẩm: Ứng dụng sẽ sử dụng dữ liệu mẫu từ InMemory (đã được seed)
- Đặt hàng: Sẽ hiển thị lỗi và không xóa giỏ hàng

## Kiểm tra kết nối API

1. Đảm bảo backend ASP.NET MVC đang chạy
2. Kiểm tra CORS settings nếu chạy trên WebAssembly
3. Kiểm tra firewall và network settings
4. Xem logs trong Debug Output để kiểm tra lỗi kết nối

## Troubleshooting

### Lỗi: "API call failed"
- Kiểm tra URL có đúng không
- Kiểm tra backend có đang chạy không
- Kiểm tra CORS settings

### Lỗi: "Timeout"
- Tăng timeout trong ApiService (hiện tại 30 giây)
- Kiểm tra network connection

### WebAssembly: CORS Error
- Cần cấu hình CORS trên backend để cho phép origin của WebAssembly app
- Thêm vào `Startup.cs` hoặc `Program.cs`:
  ```csharp
  builder.Services.AddCors(options =>
  {
      options.AddPolicy("AllowWebAssembly", policy =>
      {
          policy.WithOrigins("https://localhost:5001") // URL của WebAssembly app
                .AllowAnyMethod()
                .AllowAnyHeader();
      });
  });
  ```

