USE [EKartDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CanceledOrders]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CanceledOrders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CanceledOrders](
	[CancelId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[OrderStatus] [varchar](50) NOT NULL,
	[ReasonForCancel] [varchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created_At] [datetime] NOT NULL,
	[Updated_At] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CancelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[ParentCategoryId] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ContactUsDetails]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUsDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactUsDetails](
	[ConatctusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ContactUsDetails] PRIMARY KEY CLUSTERED 
(
	[ConatctusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ErrorLogs]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorLogs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ErrorLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[Module] [nvarchar](200) NULL,
	[ErrorType] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrderDeliveryDetails]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDeliveryDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderDeliveryDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[DispatchDate] [date] NOT NULL,
	[PostOfficeBranch] [nvarchar](255) NOT NULL,
	[TrackingNumber] [nvarchar](100) NULL,
	[ExpectedDeliveryDate] [date] NOT NULL,
	[SenderContact] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 15-04-2026 19:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DeliveryDescription] [varchar](255) NULL,
	[Totalprice] [int] NULL,
	[PaymentMethod] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[RazorpayOrderId] [varchar](100) NULL,
	[FailureReason] [varchar](255) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrderRefunds]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderRefunds]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderRefunds](
	[RefundId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[RefundPaymentType] [varchar](10) NOT NULL,
	[BankAccountName] [varchar](100) NULL,
	[BankAccountNumber] [varchar](50) NULL,
	[BankIFSC] [varchar](20) NULL,
	[UPIId] [varchar](50) NULL,
	[RefundAmount] [decimal](18, 2) NULL,
	[Status] [varchar](50) NULL,
	[Created_At] [datetime] NULL,
	[Updated_At] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RefundId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Orders](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[productId] [nvarchar](max) NOT NULL,
	[addressId] [int] NOT NULL,
	[paymentMethod] [nvarchar](max) NOT NULL,
	[quantity] [nvarchar](max) NOT NULL,
	[price] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[OrderStatus] [nvarchar](max) NOT NULL,
	[PaymentStatus] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrdersProduct]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrdersProduct]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrdersProduct](
	[OrderedProductId] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductName] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[ImageUrl] [varchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[slug] [nvarchar](100) NOT NULL,
	[sequence] [nvarchar](100) NOT NULL,
	[status] [bit] NOT NULL,
	[created_At] [datetime] NOT NULL,
	[updated_At] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[password_reset]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[password_reset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[password_reset](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[otp] [varchar](6) NOT NULL,
	[expires_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShippingAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ShippingAddress](
	[ShippingAddressId] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[MobileNumber] [nvarchar](max) NOT NULL,
	[AlternateMobileNumber] [nvarchar](max) NULL,
	[PinCode] [int] NOT NULL,
	[HouseNo] [nvarchar](max) NOT NULL,
	[AreaDetails] [nvarchar](max) NOT NULL,
	[Landmark] [nvarchar](max) NULL,
	[City] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[TypeOfAddress] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbl_cart]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cart]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tbl_cart] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbl_ContactInfo]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ContactInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_ContactInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactType] [nvarchar](10) NULL,
	[Value] [nvarchar](255) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbl_ContactSettings]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ContactSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_ContactSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryMessage] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbl_DeliveryAddress]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_DeliveryAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_DeliveryAddress](
	[AddId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[MobileNumber] [nvarchar](max) NOT NULL,
	[AlternateMobileNumber] [nvarchar](max) NULL,
	[PinCode] [int] NOT NULL,
	[HouseNo] [nvarchar](max) NOT NULL,
	[AreaDetails] [nvarchar](max) NOT NULL,
	[Landmark] [nvarchar](max) NULL,
	[City] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[TypeOfAddress] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[createAt] [datetime2](7) NOT NULL,
	[updateAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tbl_DeliveryAddress] PRIMARY KEY CLUSTERED 
(
	[AddId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbl_products]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Offer] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Availability] [nvarchar](max) NOT NULL,
	[DeliverySpan] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[Benefits] [nvarchar](max) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[topSelling] [bit] NULL,
	[trendingProduct] [bit] NULL,
	[recentlyAdded] [bit] NULL,
	[Enquiry] [bit] NOT NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_tbl_products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tbl_users]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[MobileNumber] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[isActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tbl_users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TblCategory]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TblCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Brief] [nvarchar](max) NOT NULL,
	[Icons] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TblCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[WishList]    Script Date: 15-04-2026 19:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WishList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WishList](
	[WishId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WishList] PRIMARY KEY CLUSTERED 
(
	[WishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__CanceledO__IsAct__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CanceledOrders] ADD  DEFAULT ((1)) FOR [IsActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__CanceledO__Creat__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CanceledOrders] ADD  DEFAULT (getdate()) FOR [Created_At]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Categorie__Creat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (getdate()) FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Categorie__Updat__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__ContactUs__Creat__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactUsDetails] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__ContactUs__IsAct__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactUsDetails] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__ContactUs__Updat__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactUsDetails] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [UpdatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__ErrorLogs__Creat__4F47C5E3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ErrorLogs] ADD  DEFAULT (getdate()) FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__OrderDeli__Creat__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderDeliveryDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__OrderDeli__Updat__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderDeliveryDetails] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__OrderRefu__Statu__625A9A57]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderRefunds] ADD  DEFAULT ('Pending') FOR [Status]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__OrderRefu__Creat__634EBE90]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderRefunds] ADD  DEFAULT (getdate()) FOR [Created_At]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Orders__OrderSta__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [OrderStatus]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Orders__PaymentS__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [PaymentStatus]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__OrderStat__creat__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderStatus] ADD  DEFAULT (getdate()) FOR [created_At]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__OrderStat__updat__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderStatus] ADD  DEFAULT (getdate()) FOR [updated_At]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tbl_Conta__Creat__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbl_ContactInfo] ADD  DEFAULT (getdate()) FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tbl_Conta__Creat__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbl_ContactSettings] ADD  DEFAULT (getdate()) FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tbl_produ__Benef__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT (N'') FOR [Benefits]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tbl_produ__Categ__7A672E12]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT ((0)) FOR [CategoryId]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tbl_produ__Enqui__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT ((0)) FOR [Enquiry]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tbl_produ__IsUse__6442E2C9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT ((0)) FOR [IsUsed]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ParentCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Categories]'))
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_ParentCategory] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ParentCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Categories]'))
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_ParentCategory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__password___UserI__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[password_reset]'))
ALTER TABLE [dbo].[password_reset]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[tbl_users] ([UserId])
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbl_users_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbl_users]'))
ALTER TABLE [dbo].[tbl_users]  WITH CHECK ADD  CONSTRAINT [FK_tbl_users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tbl_users_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[tbl_users]'))
ALTER TABLE [dbo].[tbl_users] CHECK CONSTRAINT [FK_tbl_users_Roles]
GO
	