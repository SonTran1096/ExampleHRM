USE [dbHrmExample]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/8/2021 11:32:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](250) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[PromoPrice] [decimal](18, 0) NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity], [Price], [PromoPrice], [Status]) VALUES (1, N'Áo thun Polo', 100, CAST(199000 AS Decimal(18, 0)), CAST(99000 AS Decimal(18, 0)), 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity], [Price], [PromoPrice], [Status]) VALUES (2, N'Quần Kaki', 10, CAST(50000 AS Decimal(18, 0)), CAST(45000 AS Decimal(18, 0)), 0)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity], [Price], [PromoPrice], [Status]) VALUES (3, N'Quần Adidas', 9, CAST(1200000 AS Decimal(18, 0)), CAST(990000 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_getListProduct]    Script Date: 5/8/2021 11:32:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		sontn
-- Create date: 05/05/2021
-- Description:	
-- exec [dbo].[sp_getListProduct] N'Áo'
-- =============================================
CREATE PROCEDURE [dbo].[sp_getListProduct]
	@KeyWord nvarchar(100) null
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY;

	select * from Product where ProductName like N'%' + @KeyWord + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProduct]    Script Date: 5/8/2021 11:32:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		sontn
-- Create date: 06/05/2021
-- Description:	
-- exec [dbo].[sp_getProduct]
-- =============================================
create PROCEDURE [dbo].[sp_getProduct]
	@OperationType int,
	@ProductId int,
	@ProductName nvarchar(250),
	@Quantity int,
	@Price decimal,
	@PromoPrice decimal,
	@Status int
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY;

	if(@OperationType = 1) -- insert
	begin
		insert into Product(ProductName, Quantity, Price, PromoPrice, Status)
		values(@ProductName, @Quantity, @Price, @PromoPrice, @Status)
	end
	else if(@OperationType = 2) -- update
	begin
		update Product
		set ProductName = @ProductName,
			Quantity = @Quantity,
			Price = @Price,
			PromoPrice = @PromoPrice,
			[Status] = @Status
		where ProductId = @ProductId
	end
	else if(@OperationType = 3) -- delete
	begin
		delete from Product
		where ProductId = @ProductId
	end
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductWithId]    Script Date: 5/8/2021 11:32:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		sontn
-- Create date: 05/05/2021
-- Description:	
-- exec [dbo].[sp_getProductWithId]
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProductWithId]
	@ProductId int
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY;

	select * from Product where ProductId = @ProductId
END
GO
