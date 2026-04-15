USE [EKartDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[UpdateRazorpayOrderIdByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateRazorpayOrderIdByOrderId]
GO
/****** Object:  StoredProcedure [dbo].[UpdateRazorpayOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateRazorpayOrderId]
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductStatus]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateProductStatus]
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductAvailability]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateProductAvailability]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePaymentStatus]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdatePaymentStatus]
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderToFailed]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateOrderToFailed]
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderStatusByRazorpayOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateOrderStatusByRazorpayOrderId]
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderStatus]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateOrderStatus]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateCategory]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCartQuantity]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateCartQuantity]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserPhoneNumber]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[Sp_UpdateUserPhoneNumber]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserPassword]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_UpdateUserPassword]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserName]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[Sp_UpdateUserName]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserEmail]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[Sp_UpdateUserEmail]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertCartItems]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_InsertCartItems]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetRecentUsers]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[Sp_GetRecentUsers]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetRecentProducts]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[Sp_GetRecentProducts]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetRecentOrders]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[Sp_GetRecentOrders]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProductsByCategoryId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetProductsByCategoryId]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOrderCancelInfo]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetOrderCancelInfo]
GO
/****** Object:  StoredProcedure [dbo].[sp_CancelOrder]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_CancelOrder]
GO
/****** Object:  StoredProcedure [dbo].[InsertUserIfNotExists]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[InsertUserIfNotExists]
GO
/****** Object:  StoredProcedure [dbo].[InsertOrderDetails]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[InsertOrderDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetUserWishlistProducts]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetUserWishlistProducts]
GO
/****** Object:  StoredProcedure [dbo].[GetUsersRowCount]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetUsersRowCount]
GO
/****** Object:  StoredProcedure [dbo].[GetUsersByPageNoOrdersCount]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetUsersByPageNoOrdersCount]
GO
/****** Object:  StoredProcedure [dbo].[GetUserFirstNameByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetUserFirstNameByOrderId]
GO
/****** Object:  StoredProcedure [dbo].[GetSubcategoriesByCategoryId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetSubcategoriesByCategoryId]
GO
/****** Object:  StoredProcedure [dbo].[GetProductStatus]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetProductStatus]
GO
/****** Object:  StoredProcedure [dbo].[GetProductsRowCount]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetProductsRowCount]
GO
/****** Object:  StoredProcedure [dbo].[GetProductsByTopCategory]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetProductsByTopCategory]
GO
/****** Object:  StoredProcedure [dbo].[getproducts]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[getproducts]
GO
/****** Object:  StoredProcedure [dbo].[GetParentCategoriesByProductId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetParentCategoriesByProductId]
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersRowCount]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetOrdersRowCount]
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersByUserId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetOrdersByUserId]
GO
/****** Object:  StoredProcedure [dbo].[getordersByPageNoOrdersCount]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[getordersByPageNoOrdersCount]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderRefundDetailsByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetOrderRefundDetailsByOrderId]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetailsByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetOrderDetailsByOrderId]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderByRazorpayOrderId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetOrderByRazorpayOrderId]
GO
/****** Object:  StoredProcedure [dbo].[getCategoriesByPageNoOrdersCount]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[getCategoriesByPageNoOrdersCount]
GO
/****** Object:  StoredProcedure [dbo].[GetCartDetailsByUserId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetCartDetailsByUserId]
GO
/****** Object:  StoredProcedure [dbo].[GetCancelledOrders]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetCancelledOrders]
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrders_SP]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetAllOrders_SP]
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrders]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetAllOrders]
GO
/****** Object:  StoredProcedure [dbo].[DeleteWishListByProductId]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteWishListByProductId]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategoryWithSubcategories]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteCategoryWithSubcategories]
GO
/****** Object:  StoredProcedure [dbo].[DeactivateDeliveryAddress]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[DeactivateDeliveryAddress]
GO
/****** Object:  StoredProcedure [dbo].[CheckWishlistExistence]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[CheckWishlistExistence]
GO
/****** Object:  StoredProcedure [dbo].[AddToWishList]    Script Date: 15-04-2026 19:39:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[AddToWishList]
GO
/****** Object:  StoredProcedure [dbo].[AddToWishList]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddToWishList]
    @UserId INT,
    @ProductId INT,
    @Message VARCHAR(100) OUTPUT
AS
BEGIN
    -- Check if the product already exists in the wishlist for the user
    IF EXISTS (SELECT 1 FROM WishList WHERE UserId = @UserId AND ProductId = @ProductId)
    BEGIN
        -- Product already exists in the wishlist
        SET @Message = 'Product already exists in the wishlist.'
    END
    ELSE
    BEGIN
        -- Insert the new item into the wishlist
        INSERT INTO WishList (UserId, ProductId, IsActive, CreatedAt, UpdatedAt)
        VALUES (@UserId, @ProductId, 1, GETDATE(), GETDATE())

        -- Set success message
        SET @Message = 'Product added to wishlist successfully.'
    END
END
GO
/****** Object:  StoredProcedure [dbo].[CheckWishlistExistence]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE   PROCEDURE [dbo].[CheckWishlistExistence]
    @UserId INT,
    @ProductId INT
AS
BEGIN
    -- Check if any record exists with the provided UserId and ProductId
    IF EXISTS (
        SELECT 1
        FROM [dbo].[WishList]
        WHERE [UserId] = @UserId
          AND [ProductId] = @ProductId
          AND [IsActive] = 1 -- Assuming you want to check only active records
    )
    BEGIN
        SELECT  'Exist' AS Message
    END;
    ELSE
    BEGIN
        SELECT 'Doesn''t Exist' AS Message;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[DeactivateDeliveryAddress]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeactivateDeliveryAddress]
    @AddId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[tbl_DeliveryAddress]
    SET IsActive = 0
    WHERE AddId = @AddId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategoryWithSubcategories]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCategoryWithSubcategories]
    @CategoryId INT
AS
BEGIN
    -- Set up recursive delete for subcategories
    WITH RecursiveCategories AS (
        SELECT CategoryId
        FROM Categories
        WHERE CategoryId = @CategoryId
       
        UNION ALL
       
        SELECT c.CategoryId
        FROM Categories c
        INNER JOIN RecursiveCategories rc ON c.ParentCategoryId = rc.CategoryId
    )
   
    -- Delete all categories in the hierarchy (including the parent)
    DELETE FROM Categories
    WHERE CategoryId IN (SELECT CategoryId FROM RecursiveCategories);
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteWishListByProductId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteWishListByProductId]
    @ProductId INT
AS
BEGIN
    DELETE FROM [dbo].[WishList]
    WHERE [ProductId] = @ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrders]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE     PROCEDURE [dbo].[GetAllOrders]
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Fetch all orders (excluding Cancelled) sorted by CreatedAt (most recent first)
    SELECT
        o.OrderID AS order_id,
        o.UserID AS consumer_id,
        sa.ShippingAddressId AS shipping_address_id,
        sa.ShippingAddressId AS billing_address_id,
        o.DeliveryDescription,
        o.PaymentMethod,
        o.Status AS payment_status,
        o.CreatedAt AS created_at,
        o.Status AS order_status,
        (SELECT SUM(op.Price * op.Quantity)
         FROM OrdersProduct op
         WHERE op.OrderID = o.OrderID) AS Totalprices
    FROM OrderDetails o
    LEFT JOIN ShippingAddress sa ON o.OrderID = sa.OrderID
    INNER JOIN OrderStatus os ON o.OrderID = os.OrderId
    WHERE os.Name != 'Cancelled'
    ORDER BY o.CreatedAt DESC;

    -- 2. Fetch products for these orders
    SELECT
        op.OrderID,
        o.UserID AS consumer_id,
        op.ProductId AS id,
        op.ProductName AS name,
        op.Description AS description,
        op.Price AS price,
        op.ImageUrl AS product_thumbnail,
        op.Quantity,
        op.Price AS sale_price,
        op.Price * op.Quantity AS sub_total,
        0 AS discount,
        'In Stock' AS stock_status,
        'Unit' AS unit,
        'Slug' AS slug,
        'Type' AS type,
        0 AS weight,
        1 AS status
    FROM OrdersProduct op
    INNER JOIN OrderDetails o ON op.OrderID = o.OrderID
    INNER JOIN OrderStatus os ON o.OrderID = os.OrderId
    WHERE os.Name != 'Cancelled';

    -- 3. Fetch shipping and billing addresses for all non-cancelled orders
    SELECT
        sa.ShippingAddressId AS id,
        o.UserID AS user_id,
        sa.FullName AS Title,
        sa.AreaDetails AS Street,
        sa.State,
        sa.City,
        sa.PinCode AS Pincode,
        sa.MobileNumber AS Phone
    FROM ShippingAddress sa
    INNER JOIN OrderDetails o ON sa.OrderID = o.OrderID
    INNER JOIN OrderStatus os ON o.OrderID = os.OrderId
    WHERE os.Name != 'Cancelled';

    -- 4. Fetch order status (excluding Cancelled)
    SELECT
        os.Id,
        os.OrderId,
        os.Name,
        os.Slug,
        os.Status
    FROM OrderStatus os
    WHERE os.Name != 'Cancelled';
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrders_SP]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllOrders_SP]
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch and sort all orders by CreatedAt (most recent first)
    SELECT 
        o.OrderID AS order_id,
        o.UserID AS consumer_id,
        sa.ShippingAddressId AS shipping_address_id,
        sa.ShippingAddressId AS billing_address_id,
        o.DeliveryDescription,
        o.PaymentMethod,
        o.Status AS payment_status, 
        o.CreatedAt AS created_at,
        o.Status AS order_status, 
        (SELECT SUM(op.Price * op.Quantity) 
         FROM OrdersProduct op 
         WHERE op.OrderID = o.OrderID) AS Totalprices
    FROM OrderDetails o
    LEFT JOIN ShippingAddress sa ON o.OrderID = sa.OrderID
    ORDER BY o.CreatedAt DESC;

    -- Fetch all products for all orders
    SELECT 
        op.OrderID,
        op.ProductId AS id,
        op.ProductName AS name,
        op.Description AS description,
        op.Price AS price,
        op.ImageUrl AS product_thumbnail,
        op.Quantity,
        op.Price AS sale_price,
        op.Price * op.Quantity AS sub_total,
        0 AS discount,
        'In Stock' AS stock_status,
        'Unit' AS unit,
        'Slug' AS slug,
        'Type' AS type,
        0 AS weight,
        1 AS status
    FROM OrdersProduct op;

    -- Fetch all shipping and billing addresses related to orders
    SELECT 
        sa.ShippingAddressId AS id,
        o.UserID AS user_id,
        sa.FullName AS Title,
        sa.AreaDetails AS Street,
        sa.State,
        sa.City,
        sa.PinCode AS Pincode,
        sa.MobileNumber AS Phone
    FROM ShippingAddress sa
    INNER JOIN OrderDetails o ON sa.OrderID = o.OrderID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetCancelledOrders]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[GetCancelledOrders]
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Fetch all orders (excluding Cancelled) sorted by CreatedAt (most recent first)
    SELECT
        o.OrderID AS order_id,
        o.UserID AS consumer_id,
        sa.ShippingAddressId AS shipping_address_id,
        sa.ShippingAddressId AS billing_address_id,
        o.DeliveryDescription,
        o.PaymentMethod,
        o.Status AS payment_status,
        o.CreatedAt AS created_at,
        o.Status AS order_status,
        (SELECT SUM(op.Price * op.Quantity)
         FROM OrdersProduct op
         WHERE op.OrderID = o.OrderID) AS Totalprices
    FROM OrderDetails o
    LEFT JOIN ShippingAddress sa ON o.OrderID = sa.OrderID
    INNER JOIN OrderStatus os ON o.OrderID = os.OrderId
    WHERE os.Name = 'Cancelled'
    ORDER BY o.CreatedAt DESC;

    -- 2. Fetch products for these orders
    SELECT
        op.OrderID,
        o.UserID AS consumer_id,
        op.ProductId AS id,
        op.ProductName AS name,
        op.Description AS description,
        op.Price AS price,
        op.ImageUrl AS product_thumbnail,
        op.Quantity,
        op.Price AS sale_price,
        op.Price * op.Quantity AS sub_total,
        0 AS discount,
        'In Stock' AS stock_status,
        'Unit' AS unit,
        'Slug' AS slug,
        'Type' AS type,
        0 AS weight,
        1 AS status
    FROM OrdersProduct op
    INNER JOIN OrderDetails o ON op.OrderID = o.OrderID
    INNER JOIN OrderStatus os ON o.OrderID = os.OrderId
    WHERE os.Name = 'Cancelled';

    -- 3. Fetch shipping and billing addresses for all non-cancelled orders
    SELECT
        sa.ShippingAddressId AS id,
        o.UserID AS user_id,
        sa.FullName AS Title,
        sa.AreaDetails AS Street,
        sa.State,
        sa.City,
        sa.PinCode AS Pincode,
        sa.MobileNumber AS Phone
    FROM ShippingAddress sa
    INNER JOIN OrderDetails o ON sa.OrderID = o.OrderID
    INNER JOIN OrderStatus os ON o.OrderID = os.OrderId
    WHERE os.Name = 'Cancelled';

    -- 4. Fetch order status (excluding Cancelled)
    SELECT
        os.Id,
        os.OrderId,
        os.Name,
        os.Slug,
        os.Status
    FROM OrderStatus os
    WHERE os.Name = 'Cancelled';

	SELECT orf.OrderId, orf.RefundId, orf.Status From OrderRefunds orf
END;
GO
/****** Object:  StoredProcedure [dbo].[GetCartDetailsByUserId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCartDetailsByUserId] (@UserId INT)
AS
BEGIN
    SELECT 
        c.CartId,
        c.UserId,
        c.ProductId,
        c.Quantity,
        p.ProductName,
        p.Description,
        p.Price,
        -- Calculate Sale_Price (Price after discount)
        (p.Price - (p.Price * ISNULL(p.Offer, 0) / 100.0)) AS Sale_Price,
        p.Availability,
        p.DeliverySpan,
        p.ImageUrl As original_url,
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt,
        p.Benefits,
        p.CategoryId,
        p.TopSelling,
        p.TrendingProduct,
        p.RecentlyAdded,
        -- Calculate SubTotal (Sale Price * Quantity)
        ((p.Price - (p.Price * ISNULL(p.Offer, 0) / 100.0)) * c.Quantity) AS SubTotal,
        p.ImageUrl AS ProductThumbnail 
    FROM 
        dbo.tbl_cart c
    JOIN 
        dbo.tbl_products p ON c.ProductId = p.ProductId
    WHERE 
        c.UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[getCategoriesByPageNoOrdersCount]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[getCategoriesByPageNoOrdersCount]
    @pageno INT,
    @pagesize INT
AS
BEGIN
    DECLARE @startRowNumber INT
    DECLARE @endRowNumber INT

    SET @endRowNumber = @pageno * @pagesize
    SET @startRowNumber = @endRowNumber - @pagesize + 1

    BEGIN
        WITH Categories
        AS (
            SELECT
                ROW_NUMBER() OVER(ORDER BY CategoryId ASC) AS RowNumber,
                 CategoryId
,CategoryName
,Brief
,Icons
,Priority
,IsActive
,CreatedAt
,UpdatedAt
            FROM
                TblCategory
        )

        SELECT
                 CategoryId
,CategoryName
,Brief
,Icons
,Priority
,IsActive
,CreatedAt
,UpdatedAt
        FROM
            Categories
        WHERE
            RowNumber >= @startRowNumber
            AND RowNumber <= @endRowNumber;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrderByRazorpayOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOrderByRazorpayOrderId]
    @RazorpayOrderId VARCHAR(100)
AS
BEGIN
    SELECT 
        [OrderID],
        [UserID],
        [DeliveryDescription],
        [Totalprice],
        [PaymentMethod],
        [Status],
        [CreatedAt],
        [RazorpayOrderId],
        [FailureReason]
    FROM [dbo].[OrderDetails]
    WHERE RazorpayOrderId = @RazorpayOrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetailsByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOrderDetailsByOrderId]
    @OrderID INT
AS
BEGIN
    -- Get Ordered Products
    SELECT
        [OrderedProductId],
        [OrderID],
        [ProductId],
        [ProductName],
        [Description],
        [Quantity],
        [Price],
        [ImageUrl]
    FROM [dbo].[OrdersProduct]
    WHERE OrderID = @OrderID;

    -- Get Shipping Address
    SELECT
        [ShippingAddressId],
        [OrderID],
        [FullName],
        [MobileNumber],
        [AlternateMobileNumber],
        [PinCode],
        [HouseNo],
        [AreaDetails],
        [Landmark],
        [City],
        [State],
        [TypeOfAddress],
        [IsActive]
    FROM [dbo].[ShippingAddress]
    WHERE OrderID = @OrderID;

    -- Get Order Delivery Details
    SELECT
        [Id],
        [OrderId],
        [FullName],
        [DispatchDate],
        [PostOfficeBranch],
        [TrackingNumber],
        [ExpectedDeliveryDate],
        [SenderContact],
        [CreatedAt],
        [UpdatedAt]
    FROM [dbo].[OrderDeliveryDetails]
    WHERE OrderId = @OrderID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderRefundDetailsByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetOrderRefundDetailsByOrderId]
    @OrderID INT
AS
BEGIN
    -- Get Ordered Products
    SELECT
        [OrderedProductId],
        [OrderID],
        [ProductId],
        [ProductName],
        [Description],
        [Quantity],
        [Price],
        [ImageUrl]
    FROM [dbo].[OrdersProduct]
    WHERE OrderID = @OrderID;

    -- Get Shipping Address
    SELECT
        [ShippingAddressId],
        [OrderID],
        [FullName],
        [MobileNumber],
        [AlternateMobileNumber],
        [PinCode],
        [HouseNo],
        [AreaDetails],
        [Landmark],
        [City],
        [State],
        [TypeOfAddress],
        [IsActive]
    FROM [dbo].[ShippingAddress]
    WHERE OrderID = @OrderID;

    -- Get Order Delivery Details
    SELECT[OrderId]
      ,[RefundPaymentType]
      ,[BankAccountName]
      ,[BankAccountNumber]
      ,[BankIFSC]
      ,[UPIId]
      ,[RefundAmount]
      ,[Status]
      ,[Created_At]
      ,[Updated_At]
    FROM [dbo].[OrderRefunds]
    WHERE OrderId = @OrderID;
END
GO
/****** Object:  StoredProcedure [dbo].[getordersByPageNoOrdersCount]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE    PROCEDURE [dbo].[getordersByPageNoOrdersCount]
    @pageno INT,
    @pagesize INT
AS
BEGIN
    DECLARE @startRowNumber INT
    DECLARE @endRowNumber INT

    SET @endRowNumber = @pageno * @pagesize
    SET @startRowNumber = @endRowNumber - @pagesize + 1

    BEGIN
        WITH OrdersDetails
        AS (
            SELECT
                ROW_NUMBER() OVER(ORDER BY orderId deSC) AS RowNumber,
orderId,
                 userId
,productId
,addressId
   ,paymentMethod
,quantity
,price,[OrderStatus]
      ,[PaymentStatus]
,IsActive
,CreatedAt
,UpdatedAt
            FROM
                Orders
        )

        SELECT
orderId,
                 userId
,productId
,addressId
   ,paymentMethod
,quantity
,price,[OrderStatus]
      ,[PaymentStatus]
,IsActive
,CreatedAt
,UpdatedAt
        FROM
            OrdersDetails
        WHERE
            RowNumber >= @startRowNumber
            AND RowNumber <= @endRowNumber;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersByUserId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE    PROCEDURE [dbo].[GetOrdersByUserId]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Fetch and sort orders by CreatedAt (most recent first)
    SELECT
        o.OrderID AS order_id,
        o.UserID AS consumer_id,
        sa.ShippingAddressId AS shipping_address_id,
        sa.ShippingAddressId AS billing_address_id,
        o.DeliveryDescription,
        o.PaymentMethod,
        o.Status AS payment_status,
        o.CreatedAt AS created_at,
        o.Status AS order_status,
        (SELECT SUM(op.Price * op.Quantity)
         FROM OrdersProduct op
         WHERE op.OrderID = o.OrderID) AS Totalprices
    FROM OrderDetails o
    LEFT JOIN ShippingAddress sa ON o.OrderID = sa.OrderID
    WHERE o.UserID = @UserId
    ORDER BY o.CreatedAt DESC;

    -- 2. Fetch products for these orders
    SELECT
        op.OrderID,
        op.ProductId AS id,
        op.ProductName AS name,
        op.Description AS description,
        op.Price AS price,
        op.ImageUrl AS product_thumbnail,
        op.Quantity,
        op.Price AS sale_price,
        op.Price * op.Quantity AS sub_total,
        0 AS discount,
        'In Stock' AS stock_status,
        'Unit' AS unit,
        'Slug' AS slug,
        'Type' AS type,
        0 AS weight,
        1 AS status
    FROM OrdersProduct op
    WHERE op.OrderID IN (
        SELECT o.OrderID FROM OrderDetails o WHERE o.UserID = @UserId
    );

    -- 3. Fetch shipping and billing addresses
    SELECT
        sa.ShippingAddressId AS id,
        o.UserID AS user_id,
        sa.FullName AS Title,
        sa.AreaDetails AS Street,
        sa.State,
        sa.City,
        sa.PinCode AS Pincode,
        sa.MobileNumber AS Phone
    FROM ShippingAddress sa
    INNER JOIN OrderDetails o ON sa.OrderID = o.OrderID
    WHERE o.OrderID IN (
        SELECT o.OrderID FROM OrderDetails o WHERE o.UserID = @UserId
    );

    -- 4. Fetch order status history
    SELECT
        os.OrderId,
        os.Id AS status_id,
        os.name AS status_name,
        os.slug,
        os.sequence,
        os.status,
        os.created_At,
        os.updated_At
    FROM OrderStatus os
    WHERE os.OrderId IN (
        SELECT o.OrderID FROM OrderDetails o WHERE o.UserID = @UserId
    )
    ORDER BY os.OrderId, os.sequence; -- Order statuses by sequence within each order
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersRowCount]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[GetOrdersRowCount]
AS
BEGIN
    SET NOCOUNT ON; -- To prevent extra result sets from being returned

    SELECT COUNT(*) AS TotalRowCount
    FROM [dbo].[Orders];
END
GO
/****** Object:  StoredProcedure [dbo].[GetParentCategoriesByProductId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetParentCategoriesByProductId]
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    WITH CategoryCTE AS (
        -- Start with the category of the product
        SELECT CategoryId, ParentCategoryId
        FROM Categories
        WHERE CategoryId = @CategoryId

        UNION ALL

        -- Recursively join to find parent categories
        SELECT c.CategoryId, c.ParentCategoryId
        FROM Categories c
        INNER JOIN CategoryCTE ct ON c.CategoryId = ct.ParentCategoryId
    )
	select CAST(CategoryId as nvarchar) as slug from CategoryCTE order by CategoryId;

END
GO
/****** Object:  StoredProcedure [dbo].[getproducts]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE   PROCEDURE [dbo].[getproducts]
    @pageno INT,
    @pagesize INT
AS
BEGIN
    DECLARE @startRowNumber INT
    DECLARE @endRowNumber INT

    SET @endRowNumber = @pageno * @pagesize
    SET @startRowNumber = @endRowNumber - @pagesize + 1

    BEGIN
        WITH OrderedProducts
        AS (
            SELECT
                ROW_NUMBER() OVER(ORDER BY ProductId desc) AS RowNumber,
                ProductId,
                ProductName,
                Description,
                Offer,
                Price,
                CategoryId,
                Availability,
                DeliverySpan,
                ImageUrl,
				Enquiry,
				IsUsed,
                IsActive,
                CreatedAt,
                UpdatedAt,
				Benefits,
				topSelling,
				trendingProduct,
				recentlyAdded
            FROM
                tbl_products where IsActive = 1
        )

        SELECT
            ProductId,
            ProductName,
            Description,
            Offer,
            Price,
            CategoryId,
            Availability,
            DeliverySpan,
            ImageUrl,
			Enquiry,
			IsUsed,
            IsActive,
            CreatedAt,
            UpdatedAt,
			Benefits,
			topSelling,
				trendingProduct,
				recentlyAdded
        FROM
            OrderedProducts
        WHERE
            RowNumber >= @startRowNumber
            AND RowNumber <= @endRowNumber;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductsByTopCategory]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductsByTopCategory]
AS
BEGIN
    -- CTE to recursively get the top-level category for each category
    WITH CategoryHierarchy AS (
        SELECT
            c.CategoryId,
            c.CategoryName,
            c.ParentCategoryId,
            c.CategoryName AS Level1CategoryName
        FROM Categories c
        WHERE c.ParentCategoryId IS NULL  -- Level 1 category

        UNION ALL

        SELECT
            c.CategoryId,
            c.CategoryName,
            c.ParentCategoryId,
            ch.Level1CategoryName  -- Carry forward Level 1 category name
        FROM Categories c
        INNER JOIN CategoryHierarchy ch ON c.ParentCategoryId = ch.CategoryId
    )

    -- Query to select products along with their top-level category name
    SELECT
        p.ProductId,
        p.ProductName,
        p.Description,
        p.Offer,
        p.Price,
        p.Availability,
        p.DeliverySpan,
        p.ImageUrl,
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt,
        p.Benefits,
        p.CategoryId,
        ch.Level1CategoryName  -- Top-level category name (e.g., 'Action')
    FROM tbl_products p
    INNER JOIN CategoryHierarchy ch ON p.CategoryId = ch.CategoryId
    ORDER BY ch.Level1CategoryName, p.ProductName;
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductsRowCount]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[GetProductsRowCount]
AS
BEGIN
    SET NOCOUNT ON; -- To prevent extra result sets from being returned

    SELECT COUNT(*) AS TotalRowCount
    FROM [dbo].[tbl_products];
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductStatus]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductStatus]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ProductId,
        ProductName,
        ImageUrl,
        topSelling,
        trendingProduct,
        recentlyAdded
    FROM tbl_products
    WHERE IsActive = 1
    ORDER BY CreatedAt DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSubcategoriesByCategoryId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetSubcategoriesByCategoryId]
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Recursive CTE to get all subcategories
    WITH SubCategoryCTE AS (
        -- Start with the given category
        SELECT CategoryId, CategoryName, ParentCategoryId
        FROM Categories
        WHERE ParentCategoryId = @CategoryId

        UNION ALL

        -- Recursively join to get subcategories of the current category
        SELECT c.CategoryId, c.CategoryName, c.ParentCategoryId
        FROM Categories c
        INNER JOIN SubCategoryCTE ct ON c.ParentCategoryId = ct.CategoryId
    )
    -- Fetch all subcategories and their parent category IDs
    SELECT CategoryId, CategoryName, ParentCategoryId FROM SubCategoryCTE;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserFirstNameByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserFirstNameByOrderId]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT u.FirstName
    FROM [EKartDB].[dbo].[OrderDetails] o
    INNER JOIN [EKartDB].[dbo].[tbl_users] u ON o.UserID = u.UserId
    WHERE o.OrderID = @OrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUsersByPageNoOrdersCount]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE   PROCEDURE [dbo].[GetUsersByPageNoOrdersCount]
    @pageno INT,
    @pagesize INT
AS
BEGIN
    SET NOCOUNT ON; -- To prevent extra result sets from being returned

    DECLARE @startRowNumber INT
    DECLARE @endRowNumber INT

    -- Calculate pagination boundaries
    SET @endRowNumber = @pageno * @pagesize
    SET @startRowNumber = @endRowNumber - @pagesize + 1

    -- Get paginated user details
    ;WITH UsersDetails AS (
        SELECT
            ROW_NUMBER() OVER(ORDER BY UserId desc) AS RowNumber,
            [UserId],
            [FirstName],
            [LastName],
            [MobileNumber],
            [Email],
            [RoleId],
            [Password],
            [isActive],
            [CreatedAt],
            [UpdatedAt]
        FROM
            [dbo].[tbl_users]
    )
    SELECT
        [UserId],
        [FirstName],
        [LastName],
        [MobileNumber],
        [Email],
        [RoleId],
        [Password],
        [isActive],
        [CreatedAt],
        [UpdatedAt],
        (SELECT COUNT(*) FROM [dbo].[tbl_users]) AS TotalRowCount -- Total row count
    FROM
        UsersDetails
    WHERE
        RowNumber >= @startRowNumber
        AND RowNumber <= @endRowNumber;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUsersRowCount]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsersRowCount]
AS
BEGIN
    SET NOCOUNT ON; -- To prevent extra result sets from being returned

    SELECT COUNT(*) AS TotalRowCount
    FROM [dbo].[tbl_users];
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserWishlistProducts]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetUserWishlistProducts]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ProductId,
        p.ProductName,
        p.Description,
        p.Offer,
        p.Price,
        p.Availability,
        p.DeliverySpan,
        p.ImageUrl,
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt,
        p.Benefits,
        p.CategoryId,
        p.topSelling,
		p.Enquiry,
        p.trendingProduct,
        p.recentlyAdded
    FROM [dbo].[tbl_products] p
    INNER JOIN [dbo].[WishList] w 
        ON p.ProductId = w.ProductId
    WHERE w.UserId = @UserId
        AND w.IsActive = 1  -- Ensuring the wishlist item is active
        AND p.IsActive = 1  -- Ensuring the product is active
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertOrderDetails]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertOrderDetails] 
    @UserID INT, 
    @DeliveryDescription VARCHAR(255),
    @PaymentMethod VARCHAR(50),
    @TotalPrice INT,
    @ShippingAddressId INT,
    @Status VARCHAR(50),
    @CreatedAt DATETIME2,
    @OrderProductDetails OrderProductDetailsType READONLY,
    @RazorpayOrderId VARCHAR(100),
    @FailureReason VARCHAR(255) = NULL
AS
BEGIN
    DECLARE @OrderID INT;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert order details
        INSERT INTO OrderDetails (
            UserID, DeliveryDescription, TotalPrice, PaymentMethod, 
            Status, CreatedAt, RazorpayOrderId, FailureReason
        )
        VALUES (
            @UserID, @DeliveryDescription, @TotalPrice, @PaymentMethod, 
            @Status, @CreatedAt, @RazorpayOrderId, @FailureReason
        );

        SET @OrderID = SCOPE_IDENTITY();

        -- Insert products
        INSERT INTO OrdersProduct (OrderID, ProductId, ProductName, Description, Quantity, Price, ImageUrl)
        SELECT 
            @OrderID, 
            opd.ProductId, 
            p.ProductName,  
            p.Description,
            opd.Quantity,
            p.Price,
            p.ImageUrl
        FROM @OrderProductDetails opd
        INNER JOIN [tbl_products] p ON opd.ProductId = p.ProductId;

        -- Insert shipping address
        INSERT INTO ShippingAddress 
        (OrderID, FullName, MobileNumber, AlternateMobileNumber, PinCode, HouseNo, 
         AreaDetails, Landmark, City, State, TypeOfAddress, IsActive)
        SELECT 
            @OrderID, 
            FullName, 
            MobileNumber, 
            AlternateMobileNumber, 
            PinCode, 
            HouseNo, 
            AreaDetails, 
            Landmark, 
            City, 
            State, 
            TypeOfAddress, 
            1  
        FROM tbl_DeliveryAddress
        WHERE AddId = @ShippingAddressId;

        -- Insert status
        INSERT INTO [dbo].[OrderStatus] (
            OrderId, name, slug, sequence, status, created_At, updated_At
        )
        VALUES (
            @OrderID, 'Pending', 'Pending', 1, 1, GETDATE(), GETDATE()
        );

        COMMIT TRANSACTION;

        -- ? Return full order details instead of just OrderId
        SELECT 
            o.OrderId,
            o.UserId,
            o.DeliveryDescription,
            o.PaymentMethod,
            o.TotalPrice,
            o.Status,
            o.CreatedAt,
            o.RazorpayOrderId,
            o.FailureReason
        FROM OrderDetails o
        WHERE o.OrderId = @OrderID;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertUserIfNotExists]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUserIfNotExists]
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @MobileNumber NVARCHAR(15),
    @Email NVARCHAR(255),
    @RoleId INT,
    @Password NVARCHAR(255),
    @isActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the user already exists by email or mobile number
    IF EXISTS (SELECT 1 FROM [dbo].[tbl_users] WHERE Email = @Email OR MobileNumber = @MobileNumber)
    BEGIN
        PRINT 'User already exists with the given email or mobile number';
        RETURN;
    END

    -- Insert new user
    INSERT INTO [dbo].[tbl_users] (
        FirstName, LastName, MobileNumber, Email, RoleId, Password, isActive, CreatedAt, UpdatedAt
    ) 
    VALUES (
        @FirstName, @LastName, @MobileNumber, @Email, @RoleId, @Password, @isActive, GETDATE(), GETDATE()
    );

    PRINT 'User inserted successfully';
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_CancelOrder]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CancelOrder]
    @OrderId INT,
    @Name VARCHAR(100),
    @Slug VARCHAR(100),
    @Sequence INT,
    @Status VARCHAR(50),
    @ReasonForCancel VARCHAR(255),
    @RefundPaymentType VARCHAR(10) = NULL, -- optional
    @BankAccountName VARCHAR(100) = NULL,
    @BankAccountNumber VARCHAR(50) = NULL,
    @BankIFSC VARCHAR(20) = NULL,
    @UPIId VARCHAR(50) = NULL,
	@RefundAmount INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1?? Update OrderStatus table
        UPDATE OrderStatus
        SET
            name = @Name,
            slug = @Slug,
            sequence = @Sequence,
            status = @Status,
            updated_At = GETDATE()
        WHERE OrderId = @OrderId;

        -- 2?? Restore product availability
        UPDATE P
        SET P.Availability = P.Availability + OP.Quantity
        FROM tbl_products P
        INNER JOIN OrdersProduct OP
            ON P.ProductId = OP.ProductId
        WHERE OP.OrderID = @OrderId;

        -- 3?? Insert into CanceledOrders
        INSERT INTO CanceledOrders
        (
            OrderId,
            OrderStatus,
            ReasonForCancel,
            IsActive,
            Created_At,
            Updated_At
        )
        VALUES
        (
            @OrderId,
            @Status,
            @ReasonForCancel,
            1,
            GETDATE(),
            NULL
        );

        -- 4?? Insert refund payment details (if provided)
        IF @RefundPaymentType IS NOT NULL
        BEGIN
            INSERT INTO OrderRefunds
            (
                OrderId,
                RefundPaymentType,
                BankAccountName,
                BankAccountNumber,
                BankIFSC,
                UPIId,
				RefundAmount,
                Status,
                Created_At
            )
            VALUES
            (
                @OrderId,
                @RefundPaymentType,
                @BankAccountName,
                @BankAccountNumber,
                @BankIFSC,
                @UPIId,
				@RefundAmount,
                'Pending',
                GETDATE()
            )
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOrderCancelInfo]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetOrderCancelInfo]
    @OrderId INT
AS
BEGIN
    SELECT 
        o.OrderID,
        u.FirstName,
        u.LastName,  -- Include LastName
        u.Email
    FROM OrderDetails o
    INNER JOIN tbl_users u ON o.UserID = u.UserId
    WHERE o.OrderID = @OrderId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProductsByCategoryId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetProductsByCategoryId]
    @CategoryId INT
AS
BEGIN
    -- Declare a temporary table to store all CategoryIds (including subcategories)
    DECLARE @CategoryIds TABLE (CategoryId INT);

    -- Recursive CTE to get all subcategories of the given CategoryId
    WITH RecursiveCategories AS (
        -- Base case: start with the given CategoryId (this ensures the parent is included)
        SELECT CategoryId
        FROM Categories
        WHERE CategoryId = @CategoryId
       
        UNION ALL
       
        -- Recursive case: find subcategories of the current CategoryId
        SELECT c.CategoryId
        FROM Categories c
        INNER JOIN RecursiveCategories rc ON c.ParentCategoryId = rc.CategoryId
    )
    -- Insert all CategoryIds from the CTE into the temporary table
    INSERT INTO @CategoryIds
    SELECT CategoryId FROM RecursiveCategories;

    -- Select products that belong to any of the found CategoryIds (including the parent)
    SELECT *
    FROM tbl_products
    WHERE CategoryId IN (SELECT CategoryId FROM @CategoryIds);
END;
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetRecentOrders]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[Sp_GetRecentOrders]
AS
BEGIN
    SELECT TOP (15) [orderId]
      ,[userId]
      ,[productId]
      ,[addressId]
      ,[paymentMethod]
      ,[quantity]
      ,[price],[OrderStatus]
      ,[PaymentStatus]
      ,[IsActive]
      ,[CreatedAt]
      ,[UpdatedAt]
  FROM [dbo].[Orders]
  order by CreatedAt desc;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetRecentProducts]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE       PROCEDURE [dbo].[Sp_GetRecentProducts]
AS
BEGIN
    --SET NOCOUNT ON;

    SELECT TOP 15
        [ProductId],
        [ProductName],
        [Description],
        [Offer],
        [Price],
        [CategoryId],
        [Availability],
        [DeliverySpan],
        [ImageUrl],
        [IsActive],
        [CreatedAt],
        [UpdatedAt]
    FROM [dbo].[tbl_products] Where IsActive=1
    ORDER BY [CreatedAt] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetRecentUsers]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



 CREATE PROCEDURE [dbo].[Sp_GetRecentUsers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 15
        [UserId],
        [FirstName],
        [LastName],
        [MobileNumber],
        [Email],
        [RoleId],
        [Password],
        [isActive],
        [CreatedAt],
        [UpdatedAt]
    FROM [dbo].[tbl_users]
    ORDER BY [CreatedAt] DESC;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertCartItems]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_InsertCartItems]
    @CartItems CartItemType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    MERGE INTO tbl_cart AS Target
    USING @CartItems AS Source
    ON Target.UserId = Source.UserId AND Target.ProductId = Source.ProductId
    WHEN MATCHED THEN
        UPDATE SET
            Target.Quantity = Target.Quantity + Source.Quantity,
            Target.CreatedDate = Source.CreatedDate
    WHEN NOT MATCHED THEN
        INSERT (UserId, ProductId, Quantity, CreatedDate)
        VALUES (Source.UserId, Source.ProductId, Source.Quantity, Source.CreatedDate);
END;
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserEmail]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

            CREATE PROCEDURE [dbo].[Sp_UpdateUserEmail]
                @UserId INT,
                @Email NVARCHAR(100)
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE [dbo].[tbl_users]
                SET [Email] = @Email,
                    [UpdatedAt] = GETUTCDATE()
                WHERE [UserId] = @UserId;
            END
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserName]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

            CREATE PROCEDURE [dbo].[Sp_UpdateUserName]
                @UserId INT,
                @FirstName NVARCHAR(50),
                @LastName NVARCHAR(50)
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE [dbo].[tbl_users]
                SET [FirstName] = @FirstName,
                    [LastName] = @LastName,
                    [UpdatedAt] = GETUTCDATE()
                WHERE [UserId] = @UserId;
            END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserPassword]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateUserPassword]
    @UserId INT,
    @NewPassword NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

   

    -- Update password (hashed)
    UPDATE [dbo].[tbl_users]
    SET [Password] = @NewPassword, 
        [UpdatedAt] = GETDATE()
    WHERE UserId = @UserId;

    PRINT 'Password updated successfully';
END;
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserPhoneNumber]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

            CREATE PROCEDURE [dbo].[Sp_UpdateUserPhoneNumber]
                @UserId INT,
                @MobileNumber NVARCHAR(20)
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE [dbo].[tbl_users]
                SET [MobileNumber] = @MobileNumber,
                    [UpdatedAt] = GETUTCDATE()
                WHERE [UserId] = @UserId;
            END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCartQuantity]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateCartQuantity]
    @UserId INT,
    @ProductId INT,
    @QuantityChange INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentQuantity INT;

    -- Get the current quantity from the cart
    SELECT @CurrentQuantity = Quantity 
    FROM tbl_cart 
    WHERE UserId = @UserId AND ProductId = @ProductId;

    -- If item exists in cart
    IF @CurrentQuantity IS NOT NULL
    BEGIN
        SET @CurrentQuantity = @CurrentQuantity + @QuantityChange;

        -- If quantity becomes 0 or less, delete the item
        IF @CurrentQuantity <= 0
        BEGIN
            DELETE FROM tbl_cart WHERE UserId = @UserId AND ProductId = @ProductId;
        END
        ELSE
        BEGIN
            -- Update quantity
            UPDATE tbl_cart
            SET Quantity = @CurrentQuantity, CreatedDate = GETUTCDATE()
            WHERE UserId = @UserId AND ProductId = @ProductId;
        END
    END
    ELSE
    BEGIN
        -- If item does not exist and quantity is positive, insert new item
        IF @QuantityChange > 0
        BEGIN
            INSERT INTO tbl_cart (UserId, ProductId, Quantity, CreatedDate)
            VALUES (@UserId, @ProductId, @QuantityChange, GETUTCDATE());
        END
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateCategory]
    @CategoryId INT,
    @CategoryName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Update the category with the new values
    UPDATE Categories
    SET
        CategoryName = @CategoryName,
        UpdatedAt = GETDATE() -- Update the 'UpdatedAt' timestamp to the current time
    WHERE
        CategoryId = @CategoryId;
   
    -- Check if the update was successful
    IF @@ROWCOUNT = 0
    BEGIN
        -- If no rows were affected, it means the category wasn't found
        RAISERROR('Category with Id %d not found.', 16, 1, @CategoryId);
        RETURN;
    END
END;


GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderStatus]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateOrderStatus]
    @OrderId INT,
    @NewOrderStatus NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Orders]
    SET OrderStatus = @NewOrderStatus,
        UpdatedAt = GETDATE()
    WHERE orderId = @OrderId;

    IF @@ROWCOUNT = 1
    BEGIN
        SELECT 'Order status updated successfully' AS Message;
    END
    ELSE
    BEGIN
        SELECT 'Order not found or update failed' AS Message;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderStatusByRazorpayOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE    PROCEDURE [dbo].[UpdateOrderStatusByRazorpayOrderId]
    @RazorpayOrderId VARCHAR(100),
    @Status VARCHAR(50),
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Step 1: Update order status
        UPDATE OrderDetails
        SET Status = @Status
        WHERE RTRIM(LTRIM(RazorpayOrderId)) = RTRIM(LTRIM(@RazorpayOrderId));

        SET @RowsAffected = @@ROWCOUNT;

        -- Step 2: Update order status in OrderStatus table
        UPDATE os
        SET 
            os.updated_At = GETDATE()
        FROM OrderStatus os
        INNER JOIN OrderDetails od ON os.OrderId = od.OrderId
        WHERE RTRIM(LTRIM(od.RazorpayOrderId)) = RTRIM(LTRIM(@RazorpayOrderId));

        -- Step 3: If status is 'Success', decrease product availability
        IF LOWER(@Status) = 'success'
        BEGIN
            DECLARE @OrderID INT, @UserId INT;

            SELECT @OrderID = OrderID, @UserId=UserID
            FROM OrderDetails
            WHERE RTRIM(LTRIM(RazorpayOrderId)) = RTRIM(LTRIM(@RazorpayOrderId));

            -- Cursor to go through each ordered product
            DECLARE cur CURSOR FOR
            SELECT ProductId, Quantity
            FROM OrdersProduct
            WHERE OrderID = @OrderID;

            DECLARE @ProductId INT, @Quantity INT;

            OPEN cur;
            FETCH NEXT FROM cur INTO @ProductId, @Quantity;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                UPDATE tbl_products
                SET Availability = Availability - @Quantity
                WHERE ProductId = @ProductId;

                FETCH NEXT FROM cur INTO @ProductId, @Quantity;
            END

            CLOSE cur;
            DEALLOCATE cur;

			-- Step 4: Delete items from the cart for this user
            DELETE FROM tbl_cart
            WHERE UserId = @UserID;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderToFailed]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateOrderToFailed]
    @RazorpayOrderId VARCHAR(100), -- Razorpay Order ID for the failed payment
    @FailureReason VARCHAR(255) = NULL -- Optional failure reason field
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Update the order status in OrderDetails table to 'Failed'
        UPDATE OrderDetails
        SET 
            Status = 'Failed',
            FailureReason = @FailureReason
        WHERE RazorpayOrderId = @RazorpayOrderId;

        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            -- If no rows were updated, raise an error
            THROW 50001, 'No order found with the given Razorpay Order ID.', 1;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- If any error occurs, roll back the transaction
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdatePaymentStatus]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[UpdatePaymentStatus]
    @OrderId INT,
    @NewPaymentStatus NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Orders]
    SET PaymentStatus = @NewPaymentStatus,
        UpdatedAt = GETDATE()
    WHERE orderId = @OrderId;

    IF @@ROWCOUNT = 1
    BEGIN
        SELECT 'Payment status updated successfully' AS Message;
    END
    ELSE
    BEGIN
        SELECT 'Payment not found or update failed' AS Message;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductAvailability]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE   PROCEDURE [dbo].[UpdateProductAvailability]
    @ProductId INT,
    @Quantity INT
AS
BEGIN
    -- Check if the product exists and has sufficient availability
    IF EXISTS (SELECT 1 FROM [dbo].[tbl_products] WHERE ProductId = @ProductId AND Availability >= @Quantity)
    BEGIN
        -- Update the availability quantity of the product
        UPDATE [dbo].[tbl_products]
        SET Availability = Availability - @Quantity,
            UpdatedAt = GETDATE()
        WHERE ProductId = @ProductId;

        -- Return success message
        SELECT 'Product availability updated successfully' AS Message;
    END
    ELSE
    BEGIN
        -- Return error message if product does not exist or insufficient availability
        SELECT 'Product not found or insufficient availability' AS Message;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductStatus]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateProductStatus]
    @ProductId INT,
    @TopSelling BIT,
    @TrendingProduct BIT,
    @RecentlyAdded BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tbl_products
    SET
        TopSelling = @TopSelling,
        TrendingProduct = @TrendingProduct,
        RecentlyAdded = @RecentlyAdded
    WHERE ProductId = @ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRazorpayOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRazorpayOrderId]
    @OrderId INT,
    @RazorpayOrderId VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE OrderDetails
    SET RazorpayOrderId = @RazorpayOrderId
    WHERE OrderId = @OrderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateRazorpayOrderIdByOrderId]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRazorpayOrderIdByOrderId]
    @OrderId INT,
    @RazorpayOrderId VARCHAR(100),
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE OrderDetails
        SET RazorpayOrderId = @RazorpayOrderId
        WHERE OrderId = @OrderId;

        SET @RowsAffected = @@ROWCOUNT;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 15-04-2026 19:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateUserDetails]
    @UserId INT,
    @FirstName NVARCHAR(255),
    @MobileNumber NVARCHAR(20),
    
    @UpdatedAt DATETIME
AS
BEGIN
    UPDATE tbl_users 
    SET FirstName = @FirstName, 
        MobileNumber = @MobileNumber, 
      
        UpdatedAt = @UpdatedAt
    WHERE UserId = @UserId;
END;
GO
