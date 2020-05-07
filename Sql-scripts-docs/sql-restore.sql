USE [master]
GO
/****** Object:  Database [DB_A53DDD_JAKOB]    Script Date: 2020-01-27 13:28:15 ******/
CREATE DATABASE [DB_A53DDD_JAKOB]
GO

USE [DB_A53DDD_JAKOB]
GO
/****** Object:  Table [dbo].[ArticleIngredients]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleIngredients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ArticleID] [int] NOT NULL,
	[IngredientID] [int] NOT NULL,
 CONSTRAINT [PK_ArticleIngredients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleOrders]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleOrders](
	[ArticleID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NULL,
	[BasePrice] [float] NULL,
	[Type] [nchar](64) NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](64) NULL,
	[LastName] [nchar](64) NULL,
	[Email] [nchar](128) NULL,
	[Password] [nchar](128) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[ID] [int] NOT NULL,
	[Name] [nchar](64) NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TimeCreated] [datetime] NOT NULL,
	[Orderstatus] [smallint] NULL,
	[Price] [float] NULL,
	[CustomerID] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestA]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestA](
	[ID] [int] IDENTITY(0,1) NOT NULL,
	[v] [varchar](100) NULL,
 CONSTRAINT [TestA_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_TimeCreated]  DEFAULT (getdate()) FOR [TimeCreated]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_Orderstatus]  DEFAULT ((1)) FOR [Orderstatus]
GO
ALTER TABLE [dbo].[ArticleIngredients]  WITH CHECK ADD  CONSTRAINT [ArticleIngredients_FK] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[Articles] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArticleIngredients] CHECK CONSTRAINT [ArticleIngredients_FK]
GO
ALTER TABLE [dbo].[ArticleIngredients]  WITH CHECK ADD  CONSTRAINT [ArticleIngredients_FK_1] FOREIGN KEY([IngredientID])
REFERENCES [dbo].[Ingredients] ([ID])
GO
ALTER TABLE [dbo].[ArticleIngredients] CHECK CONSTRAINT [ArticleIngredients_FK_1]
GO
ALTER TABLE [dbo].[ArticleOrders]  WITH CHECK ADD  CONSTRAINT [FK_ARTICLE] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[Articles] ([ID])
GO
ALTER TABLE [dbo].[ArticleOrders] CHECK CONSTRAINT [FK_ARTICLE]
GO
ALTER TABLE [dbo].[ArticleOrders]  WITH CHECK ADD  CONSTRAINT [FK_ORDER] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[ArticleOrders] CHECK CONSTRAINT [FK_ORDER]
GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployees]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE PROC GetOrderById
--@ID int
--AS
--BEGIN
--SELECT ID, TimeCreated, OrderStatus, Price,CustomerID
--FROM Orders
--END
CREATE PROC [dbo].[GetAllEmployees]
As
BEGIN
SELECT ID, [Name],LastName,Email,[Password]
FROM Employees
END
GO
/****** Object:  StoredProcedure [dbo].[getArticleIngredients]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
CREATE PROCEDURE [dbo].[getArticleIngredients]
	@articleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Ingredients.*
	FROM Articles INNER JOIN ArticleIngredients 
	ON Articles.ID = ArticleIngredients.ArticleID
	inner join Ingredients on Ingredients.ID = ArticleIngredients.IngredientID
	WHERE Articles.ID = @articleID
END
GO
/****** Object:  StoredProcedure [dbo].[getArticles]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
CREATE PROCEDURE [dbo].[getArticles]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Articles
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderById]    Script Date: 2020-01-27 13:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetOrderById]
@ID int
AS
BEGIN
SELECT ID, TimeCreated, OrderStatus, Price,CustomerID
FROM Orders
END
--CREATE PROC GetAllEmployees
--As
--BEGIN
--SELECT ID, [Name],LastName,Email,[Password]
--FROM Employees
--END
GO
USE [master]
GO
ALTER DATABASE [DB_A53DDD_JAKOB] SET  READ_WRITE 
GO
