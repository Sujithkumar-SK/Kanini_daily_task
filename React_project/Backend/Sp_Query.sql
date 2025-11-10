CREATE OR ALTER PROCEDURE sp_GetAllCustomers
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CustomerId,
        c.FirstName,
        c.MiddleName,
        c.LastName,
        c.DateOfBirth,
        c.Gender,
        c.Address,
        c.City,
        c.State,
        c.PinCode,
        c.IsActive,
        c.UserId,
        u.Email,
        u.Phone
    FROM Customers c
    INNER JOIN Users u ON c.UserId = u.UserId
    WHERE c.IsDeleted = 0
        AND u.IsDeleted = 0
    ORDER BY c.CreatedOn DESC
END
GO
--------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetCustomerById
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CustomerId,
        c.FirstName,
        c.MiddleName,
        c.LastName,
        c.DateOfBirth,
        c.Gender,
        c.Address,
        c.City,
        c.State,
        c.PinCode,
        c.IsActive,
        c.UserId,
        u.Email,
        u.Phone
    FROM Customers c
    INNER JOIN Users u ON c.UserId = u.UserId
    WHERE c.CustomerId = @CustomerId 
        AND c.IsDeleted = 0
        AND u.IsDeleted = 0
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetSubscriptionPlans
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        PlanId,
        PlanName,
        Description,
        Price,
        MaxProducts,
        DurationDays
    FROM SubscriptionPlans
    WHERE IsActive = 1 
        AND IsDeleted = 0
    ORDER BY Price ASC
END
GO
------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetUserByEmail
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
            UserId,
            Email,
            PasswordHash,
            Phone,
            Role,
            IsEmailVerified,
            IsActive,
            LastLogin,
            FailedLoginAttempts,
            LockoutEnd,
            TenantId,
            CreatedBy,
            CreatedOn,
            UpdatedBy,
            UpdatedOn
        FROM Users 
        WHERE Email = @Email 
            AND IsDeleted = 0
        ORDER BY CreatedOn DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
-----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetUserById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
            UserId,
            Email,
            PasswordHash,
            Phone,
            Role,
            IsEmailVerified,
            IsActive,
            LastLogin,
            FailedLoginAttempts,
            LockoutEnd,
            TenantId,
            CreatedBy,
            CreatedOn,
            UpdatedBy,
            UpdatedOn
        FROM Users 
        WHERE UserId = @UserId 
            AND IsDeleted = 0
        ORDER BY CreatedOn DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
----------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetUserByRefreshToken
    @RefreshToken NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
            u.UserId,
            u.Email,
            u.PasswordHash,
            u.Phone,
            u.Role,
            u.IsEmailVerified,
            u.IsActive,
            u.LastLogin,
            u.FailedLoginAttempts,
            u.LockoutEnd,
            u.TenantId,
            u.CreatedBy,
            u.CreatedOn,
            u.UpdatedBy,
            u.UpdatedOn
        FROM Users u
        INNER JOIN RefreshTokens rt ON u.UserId = rt.UserId
        WHERE rt.Token = @RefreshToken 
            AND rt.IsRevoked = 0 
            AND rt.ExpiresAt > GETUTCDATE()
            AND u.IsDeleted = 0
            AND rt.IsDeleted = 0
        ORDER BY rt.CreatedOn DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetVendorById
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        v.VendorId,
        v.BusinessName,
        v.OwnerName,
        v.BusinessLicenseNumber,
        v.BusinessAddress,
        v.City,
        v.State,
        v.PinCode,
        CASE v.Status
            WHEN 0 THEN 'PendingApproval'
            WHEN 1 THEN 'Active'
            WHEN 2 THEN 'Inactive'
            WHEN 3 THEN 'Suspended'
            WHEN 4 THEN 'Rejected'
        END as Status,
        CASE v.CurrentPlan
            WHEN 1 THEN 'Basic'
            WHEN 2 THEN 'Standard'
            WHEN 3 THEN 'Premium'
        END as CurrentPlan
    FROM Vendors v
    WHERE v.VendorId = @VendorId 
        AND v.IsDeleted = 0
END
GO
---------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetVendorByUserId
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        v.VendorId,
        v.BusinessName,
        v.OwnerName,
        v.BusinessLicenseNumber,
        v.BusinessAddress,
        v.City,
        v.State,
        v.PinCode,
        CASE v.Status
            WHEN 0 THEN 'PendingApproval'
            WHEN 1 THEN 'Active'
            WHEN 2 THEN 'Inactive'
            WHEN 3 THEN 'Suspended'
            WHEN 4 THEN 'Rejected'
        END as Status,
        CASE v.CurrentPlan
            WHEN 1 THEN 'Basic'
            WHEN 2 THEN 'Standard'
            WHEN 3 THEN 'Premium'
        END as CurrentPlan
    FROM Vendors v
    WHERE v.UserId = @UserId 
        AND v.IsDeleted = 0
END
GO
----------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_ValidateUserCredentials
    @Email NVARCHAR(150),
    @PasswordHash NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @IsValid BIT = 0;
        
        IF EXISTS (
            SELECT 1 
            FROM Users 
            WHERE Email = @Email 
                AND PasswordHash = @PasswordHash 
                AND IsDeleted = 0
                AND IsActive = 1
        )
        BEGIN
            SET @IsValid = 1;
        END
        
        SELECT @IsValid AS IsValid;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckBusinessLicenseExists
    @BusinessLicenseNumber NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT CASE 
        WHEN EXISTS (SELECT 1 FROM Vendors WHERE BusinessLicenseNumber = @BusinessLicenseNumber) 
        THEN CAST(1 AS BIT) 
        ELSE CAST(0 AS BIT) 
    END AS BusinessLicenseExists;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckEmailExists
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT CASE 
        WHEN EXISTS (SELECT 1 FROM Users WHERE Email = @Email) 
        THEN CAST(1 AS BIT) 
        ELSE CAST(0 AS BIT) 
    END AS EmailExists;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckPhoneExists
    @Phone NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT CASE 
        WHEN EXISTS (SELECT 1 FROM Users WHERE Phone = @Phone) 
        THEN CAST(1 AS BIT) 
        ELSE CAST(0 AS BIT) 
    END AS PhoneExists;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetPendingVendors
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        v.VendorId,
        v.BusinessName,
        v.OwnerName,
        v.BusinessLicenseNumber,
        v.BusinessAddress,
        v.City,
        v.State,
        v.PinCode,
        v.TaxRegistrationNumber,
        v.DocumentPath,
        u.Email,
        u.Phone,
        v.CreatedOn,
        v.Status,
        CASE v.Status
            WHEN 0 THEN 'PendingApproval'
            WHEN 1 THEN 'Active'
            WHEN 2 THEN 'Inactive'
            WHEN 3 THEN 'Suspended'
            WHEN 4 THEN 'Rejected'
        END as StatusText
    FROM Vendors v
    INNER JOIN Users u ON v.UserId = u.UserId
    WHERE v.Status = 0 -- PendingApproval
        AND v.IsDeleted = 0
        AND u.IsDeleted = 0
    ORDER BY v.CreatedOn DESC
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetVendorForApproval
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        v.VendorId,
        v.BusinessName,
        v.OwnerName,
        v.BusinessLicenseNumber,
        v.BusinessAddress,
        v.City,
        v.State,
        v.PinCode,
        v.TaxRegistrationNumber,
        v.DocumentPath,
        u.Email,
        u.Phone,
        v.CreatedOn,
        v.Status,
        CASE v.Status
            WHEN 0 THEN 'PendingApproval'
            WHEN 1 THEN 'Active'
            WHEN 2 THEN 'Inactive'
            WHEN 3 THEN 'Suspended'
            WHEN 4 THEN 'Rejected'
        END as StatusText
    FROM Vendors v
    INNER JOIN Users u ON v.UserId = u.UserId
    WHERE v.VendorId = @VendorId
        AND v.IsDeleted = 0
        AND u.IsDeleted = 0
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetVendorForApprovalWithDetails
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Vendor Information
        v.VendorId,
        v.BusinessName,
        v.OwnerName,
        v.BusinessLicenseNumber,
        v.BusinessAddress,
        v.City,
        v.State,
        v.PinCode,
        v.TaxRegistrationNumber,
        v.DocumentPath,
        v.DocumentStatus,
        v.VerifiedOn,
        v.VerifiedBy,
        v.RejectionReason,
        v.CurrentPlan,
        v.Status,
        v.IsActive,
        v.CreatedOn AS VendorCreatedOn,
        v.UpdatedOn AS VendorUpdatedOn,
        v.CreatedBy AS VendorCreatedBy,
        v.UpdatedBy AS VendorUpdatedBy,
        
        -- User Information
        u.UserId,
        u.Email,
        u.Phone,
        u.Role,
        u.IsEmailVerified,
        u.CreatedOn AS UserCreatedOn
    FROM 
        Vendors v
        INNER JOIN Users u ON v.UserId = u.UserId
    WHERE 
        v.VendorId = @VendorId
        AND v.IsDeleted = 0
        AND u.IsDeleted = 0
    ORDER BY 
        v.CreatedOn DESC;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetAllProducts
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.ProductId,
        p.Name,
        p.Description,
        p.SKU,
        p.Price,
        p.DiscountPrice,
        p.StockQuantity,
        p.MinStockLevel,
        p.Brand,
        p.Weight,
        p.Dimensions,
        p.Status,
        p.IsActive,
        p.CreatedOn,
        p.UpdatedOn,
        p.CreatedBy,
        p.UpdatedBy,
        p.VendorId,
        v.BusinessName AS VendorName,
        p.CategoryId,
        c.Name AS CategoryName,
        (SELECT TOP 1 ImagePath FROM ProductImages pi WHERE pi.ProductId = p.ProductId AND pi.IsDeleted = 0 AND pi.IsPrimary = 1) AS PrimaryImagePath
    FROM 
        Products p
        INNER JOIN Vendors v ON p.VendorId = v.VendorId
        INNER JOIN Categories c ON p.CategoryId = c.CategoryId
    WHERE 
        p.IsDeleted = 0
        AND v.IsDeleted = 0
        AND c.IsDeleted = 0
        AND p.Status = 1 -- Active products only
    ORDER BY 
        p.CreatedOn DESC;
END
GO
----------------------------------------------------------
CREATE PROCEDURE sp_GetProductById
    @ProductId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get product details with vendor and category information
    SELECT 
        p.ProductId,
        p.Name,
        p.Description,
        p.SKU,
        p.Price,
        p.DiscountPrice,
        p.StockQuantity,
        p.MinStockLevel,
        p.Brand,
        p.Weight,
        p.Dimensions,
        p.Status,
        p.IsActive,
        p.CreatedOn,
        p.UpdatedOn,
        p.CreatedBy,
        p.UpdatedBy,
        p.VendorId,
        v.BusinessName AS VendorName,
        p.CategoryId,
        c.Name AS CategoryName
    FROM 
        Products p
        INNER JOIN Vendors v ON p.VendorId = v.VendorId
        INNER JOIN Categories c ON p.CategoryId = c.CategoryId
    WHERE 
        p.ProductId = @ProductId
        AND p.IsDeleted = 0
        AND v.IsDeleted = 0
        AND c.IsDeleted = 0;
    
    -- Get product images
    SELECT 
        ImagePath
    FROM 
        ProductImages
    WHERE 
        ProductId = @ProductId
        AND IsDeleted = 0
    ORDER BY 
        CreatedOn ASC;
END
GO
----------------------------------------------------------
CREATE PROCEDURE sp_GetProductsByVendor
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.ProductId,
        p.Name,
        p.Description,
        p.SKU,
        p.Price,
        p.DiscountPrice,
        p.StockQuantity,
        p.MinStockLevel,
        p.Brand,
        p.Weight,
        p.Dimensions,
        p.Status,
        p.IsActive,
        p.CreatedOn,
        p.UpdatedOn,
        p.CreatedBy,
        p.UpdatedBy,
        p.VendorId,
        v.BusinessName AS VendorName,
        p.CategoryId,
        c.Name AS CategoryName,
        (SELECT TOP 1 ImagePath FROM ProductImages pi WHERE pi.ProductId = p.ProductId AND pi.IsDeleted = 0 ORDER BY pi.CreatedOn ASC) AS PrimaryImagePath
    FROM 
        Products p
        INNER JOIN Vendors v ON p.VendorId = v.VendorId
        INNER JOIN Categories c ON p.CategoryId = c.CategoryId
    WHERE 
        p.VendorId = @VendorId
        AND p.IsDeleted = 0
        AND v.IsDeleted = 0
        AND c.IsDeleted = 0
    ORDER BY 
        p.CreatedOn DESC;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetProductsByCategory
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.ProductId,
        p.Name,
        p.Description,
        p.SKU,
        p.Price,
        p.DiscountPrice,
        p.StockQuantity,
        p.MinStockLevel,
        p.Brand,
        p.Weight,
        p.Dimensions,
        p.Status,
        p.IsActive,
        p.CreatedOn,
        p.UpdatedOn,
        p.CreatedBy,
        p.UpdatedBy,
        p.VendorId,
        v.BusinessName AS VendorName,
        p.CategoryId,
        c.Name AS CategoryName,
        (SELECT TOP 1 ImagePath FROM ProductImages pi WHERE pi.ProductId = p.ProductId AND pi.IsDeleted = 0 ORDER BY pi.CreatedOn ASC) AS PrimaryImagePath
    FROM 
        Products p
        INNER JOIN Vendors v ON p.VendorId = v.VendorId
        INNER JOIN Categories c ON p.CategoryId = c.CategoryId
    WHERE 
        p.CategoryId = @CategoryId
        AND p.IsDeleted = 0
        AND v.IsDeleted = 0
        AND c.IsDeleted = 0
        AND p.Status = 1 -- Active products only
    ORDER BY 
        p.CreatedOn DESC;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckSKUExists
    @SKU NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM Products 
                WHERE SKU = @SKU 
                AND IsDeleted = 0
            ) 
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS SKUExists;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_ValidateCategory
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM Categories 
                WHERE CategoryId = @CategoryId 
                AND IsDeleted = 0 
                AND IsActive = 1
            ) 
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS CategoryValid;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_ValidateVendor
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM Vendors 
                WHERE VendorId = @VendorId 
                AND IsDeleted = 0 
                AND IsActive = 1
                AND Status = 1 -- Active status
            ) 
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS VendorValid;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckCartItemExists
    @CustomerId INT,
    @ProductId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (
                SELECT 1 FROM Cart 
                WHERE CustomerId = @CustomerId 
                AND ProductId = @ProductId 
                AND IsDeleted = 0
            ) 
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS ItemExists;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetCartByCustomerId
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CartId,
        c.ProductId,
        p.Name AS ProductName,
        p.SKU AS ProductSKU,
        p.Price,
        p.DiscountPrice,
        c.Quantity,
        CASE 
            WHEN p.DiscountPrice IS NOT NULL THEN p.DiscountPrice * c.Quantity
            ELSE p.Price * c.Quantity
        END AS TotalPrice,
        (SELECT TOP 1 ImagePath FROM ProductImages pi WHERE pi.ProductId = p.ProductId AND pi.IsDeleted = 0 AND pi.IsPrimary = 1) AS ProductImage,
        v.VendorId,
        v.BusinessName AS VendorName,
        p.StockQuantity,
        p.IsActive,
        c.CreatedOn AS AddedOn
    FROM 
        Cart c
        INNER JOIN Products p ON c.ProductId = p.ProductId
        INNER JOIN Vendors v ON p.VendorId = v.VendorId
    WHERE 
        c.CustomerId = @CustomerId
        AND c.IsDeleted = 0
        AND p.IsDeleted = 0
        AND v.IsDeleted = 0
    ORDER BY 
        c.CreatedOn DESC;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetAllCategories
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CategoryId,
        c.Name,
        c.Description,
        c.ImagePath,
        c.IsActive,
        c.ParentCategoryId,
        pc.Name AS ParentCategoryName,
        c.CreatedOn
    FROM Categories c
    LEFT JOIN Categories pc ON c.ParentCategoryId = pc.CategoryId
    WHERE c.IsDeleted = 0
    ORDER BY c.CreatedOn DESC;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetOrderById
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
            o.OrderId,
            o.CustomerId,
            o.TotalAmount,
            o.ShippingAddress,
            o.Status,
            o.CreatedOn,
            c.FirstName + ' ' + c.LastName AS CustomerName,
            u.Phone AS CustomerPhone,
            u.Email AS CustomerEmail
        FROM Orders o
        INNER JOIN Customers c ON o.CustomerId = c.CustomerId
        INNER JOIN Users u ON c.UserId = u.UserId
        WHERE o.OrderId = @OrderId 
            AND o.IsDeleted = 0
            AND c.IsDeleted = 0
            AND u.IsDeleted = 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckStockAvailability
    @ProductId INT,
    @RequiredQuantity INT,
    @IsAvailable BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CurrentStock INT;
    
    -- Get current stock quantity
    SELECT @CurrentStock = StockQuantity 
    FROM Products 
    WHERE ProductId = @ProductId 
      AND IsDeleted = 0 
      AND IsActive = 1
      AND Status IN (1, 2); -- Active or OutOfStock status
    
    -- Check if we have enough stock
    IF @CurrentStock IS NULL
        SET @IsAvailable = 0; -- Product not found
    ELSE IF @CurrentStock >= @RequiredQuantity
        SET @IsAvailable = 1; -- Stock available
    ELSE
        SET @IsAvailable = 0; -- Insufficient stock
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetOrderItems
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
            oi.OrderItemId,
            oi.OrderId,
            oi.ProductId,
            oi.Quantity,
            oi.UnitPrice,
            oi.TotalPrice,
            oi.CreatedOn,
            p.Name AS ProductName,
            p.SKU AS ProductSKU,
            p.Brand,
            v.BusinessName AS VendorName,
            (SELECT TOP 1 ImagePath FROM ProductImages pi WHERE pi.ProductId = p.ProductId AND pi.IsDeleted = 0 AND pi.IsPrimary = 1) AS ProductImage
        FROM OrderItems oi
        INNER JOIN Products p ON oi.ProductId = p.ProductId
        INNER JOIN Vendors v ON p.VendorId = v.VendorId
        WHERE oi.OrderId = @OrderId 
            AND oi.IsDeleted = 0
            AND p.IsDeleted = 0
            AND v.IsDeleted = 0
        ORDER BY oi.CreatedOn DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetPaymentById
    @PaymentId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.PaymentId,
        p.OrderId,
        p.PaymentMethod,
        p.Status AS PaymentStatus,
        p.Amount,
        p.TransactionId,
        p.PaymentDate,
        p.CreatedOn
    FROM Payments p
    WHERE p.PaymentId = @PaymentId;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetPaymentByRazorpayOrderId
    @RazorpayOrderId NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.PaymentId,
        p.OrderId,
        p.PaymentMethod,
        p.Status AS PaymentStatus,
        p.Amount,
        p.TransactionId,
        p.PaymentDate,
        p.CreatedOn
    FROM Payments p
    WHERE p.TransactionId = @RazorpayOrderId;
END
GO
----------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetOrdersByCustomerId
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        o.OrderId,
        o.CustomerId,
        o.TotalAmount,
        o.ShippingAddress,
        o.Status,
        o.CreatedOn
    FROM Orders o
    WHERE o.CustomerId = @CustomerId AND o.IsDeleted = 0
    ORDER BY o.CreatedOn DESC;
END
GO
----------------------------------------------------------
select * from users
select * from customers
select * from Vendors
select * from Categories
select * from RefreshTokens
select * from Products
select * from ProductImages
select * from Cart


DELETE FROM Users WHERE Email = '';
delete from Vendors where VendorId=5
delete from users where UserId=5
-- Insert new admin with properly hashed password for "Admin123"
INSERT INTO Users (Email, PasswordHash, Phone, Role, IsActive, IsEmailVerified, FailedLoginAttempts, IsDeleted, CreatedBy, CreatedOn, TenantId)
VALUES (
    'sujithkumar.kanini@outlook.com',
    '$2a$11$8gF7YQvnAb8rKtXKjxeOUeQxQxQxQxQxQxQxQxQxQxQxQxQxQxQxQx',
    '9385562091',
    3, -- Admin role
    1, -- IsActive
    1, -- IsEmailVerified
    0, -- FailedLoginAttempts
    0, -- IsDeleted
    'System',
    GETUTCDATE(),
    'admin'
);

UPDATE Users 
SET PasswordHash = 'O2Esdae1BIpDX7bsgeUv+S1teVqLWpwXBw9qY8l6U7I='
WHERE Email = 'sujithkumar.kanini@outlook.com';

Update Users set IsActive =1 where UserId=6
UPDATE Products SET Status = 1 WHERE ProductId = 1;

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;

Truncate table customers
