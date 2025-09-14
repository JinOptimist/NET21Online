CREATE INDEX IX_Products_IsActive ON Products (IsActive) INCLUDE (ProductType, Name, Brand, Price, Description, ImageUrl, CreatedDate);
CREATE INDEX IX_Products_CategoryId ON Products (CategoryId);
CREATE INDEX IX_Categories_Id ON Categories (Id);

SELECT 
    p.Id,
    p.ProductType,
    p.Name,
    p.Brand,
    p.Price,
    p.Description,
    p.ImageUrl,
    p.CreatedDate,
    p.IsActive,
    c.Name AS CategoryName,
    u.UserName AS OwnerName
FROM Products p
LEFT JOIN Categories c ON p.CategoryId = c.Id
LEFT JOIN Users u ON p.OwnerId = u.Id
WHERE p.IsActive = 1
ORDER BY p.CreatedDate DESC;