USE [DB_A53DDD_JAKOB]
GO
SET IDENTITY_INSERT [dbo].[Articles] ON 
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (1, N'Vesuvio', 99, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (2, N'Margarita', 85, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (3, N'Hawaii', 85, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (4, N'Calzone', 85, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (5, N'Napoli', 90, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (6, N'Kebabpizza', 90, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (7, N'Inadiana', 95, N'Pizza                                                           ')
GO
INSERT [dbo].[Articles] ([ID], [Name], [BasePrice], [Type]) VALUES (8, N'Capricciosa', 85, N'Pizza                                                           ')
GO
SET IDENTITY_INSERT [dbo].[Articles] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([ID], [TimeCreated], [Orderstatus], [Price], [CustomerID]) VALUES (2, CAST(N'2020-01-24T13:25:23.290' AS DateTime), 2, 0, 999)
GO
INSERT [dbo].[Orders] ([ID], [TimeCreated], [Orderstatus], [Price], [CustomerID]) VALUES (3, CAST(N'2020-01-24T13:26:33.037' AS DateTime), 2, 0, 999)
GO
INSERT [dbo].[Orders] ([ID], [TimeCreated], [Orderstatus], [Price], [CustomerID]) VALUES (4, CAST(N'2020-01-24T13:43:04.367' AS DateTime), 2, 0, 999)
GO
INSERT [dbo].[Orders] ([ID], [TimeCreated], [Orderstatus], [Price], [CustomerID]) VALUES (5, CAST(N'2020-01-24T13:44:39.253' AS DateTime), 2, 0, 999)
GO
INSERT [dbo].[Orders] ([ID], [TimeCreated], [Orderstatus], [Price], [CustomerID]) VALUES (6, CAST(N'2020-01-24T13:50:08.807' AS DateTime), 2, 0, 999)
GO
INSERT [dbo].[Orders] ([ID], [TimeCreated], [Orderstatus], [Price], [CustomerID]) VALUES (8, CAST(N'2020-01-24T13:56:47.253' AS DateTime), 3, 99, 99)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ArticleOrders] ON 
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (6, 2, 2)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (1, 2, 3)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (2, 2, 4)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (3, 2, 5)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (4, 2, 6)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (2, 2, 7)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (2, 3, 8)
GO
INSERT [dbo].[ArticleOrders] ([ArticleID], [OrderID], [ID]) VALUES (2, 4, 9)
GO
SET IDENTITY_INSERT [dbo].[ArticleOrders] OFF
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (1, N'Ost                                                             ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (2, N'Tomatsås                                                        ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (3, N'Skinka                                                          ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (4, N'Champinjoner                                                    ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (5, N'Ananas                                                          ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (6, N'Kebabkött                                                       ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (7, N'Svamp                                                           ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (8, N'Spenat                                                          ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (9, N'Mozzarella                                                      ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (10, N'Extra_Ost                                                       ', 0)
GO
INSERT [dbo].[Ingredients] ([ID], [Name], [Price]) VALUES (11, N'Size_XL                                                         ', 40)
GO
SET IDENTITY_INSERT [dbo].[ArticleIngredients] ON 
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (7, 1, 1)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (8, 1, 2)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (9, 1, 3)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (10, 2, 7)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (11, 3, 5)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (12, 4, 2)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (13, 5, 8)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (14, 6, 11)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (15, 7, 5)
GO
INSERT [dbo].[ArticleIngredients] ([ID], [ArticleID], [IngredientID]) VALUES (16, 8, 9)
GO
SET IDENTITY_INSERT [dbo].[ArticleIngredients] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([ID], [Name], [LastName], [Email], [Password]) VALUES (1, N'Fredrik                                                         ', N'Molle                                                           ', N'molle@kth.se                                                                                                                    ', N'123                                                                                                                             ')
GO
INSERT [dbo].[Employees] ([ID], [Name], [LastName], [Email], [Password]) VALUES (2, N'Meles                                                           ', N'Bitow                                                           ', N'Bitow@kth.se                                                                                                                    ', N'234                                                                                                                             ')
GO
INSERT [dbo].[Employees] ([ID], [Name], [LastName], [Email], [Password]) VALUES (3, N'Jacob                                                           ', N'Andres                                                          ', N'jacob@kth.se                                                                                                                    ', N'456                                                                                                                             ')
GO
INSERT [dbo].[Employees] ([ID], [Name], [LastName], [Email], [Password]) VALUES (4, N'Firas                                                           ', N'Hans                                                            ', N'haans@kth.se                                                                                                                    ', N'678                                                                                                                             ')
GO
INSERT [dbo].[Employees] ([ID], [Name], [LastName], [Email], [Password]) VALUES (5, N'Jesse                                                           ', N'Petersson                                                       ', N'jesse@Newton.se                                                                                                                 ', N'891                                                                                                                             ')
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
