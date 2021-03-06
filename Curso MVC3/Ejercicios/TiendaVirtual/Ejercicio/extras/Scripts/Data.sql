USE [TiendaVirtual]
GO

SET IDENTITY_INSERT [dbo].[Categorias] ON
INSERT [dbo].[Categorias] ([Id], [Nombre]) VALUES (1, N'Libros')
INSERT [dbo].[Categorias] ([Id], [Nombre]) VALUES (2, N'Películas y Música')
INSERT [dbo].[Categorias] ([Id], [Nombre]) VALUES (3, N'Deportes')
SET IDENTITY_INSERT [dbo].[Categorias] OFF

SET IDENTITY_INSERT [dbo].[Productos] ON
INSERT [dbo].[Productos] ([Id], [Nombre], [Precio], [CategoriaId]) VALUES (2, N'Hola Mundo! Computación para niños', CAST(25.50 AS Decimal(8, 2)), 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Precio], [CategoriaId]) VALUES (4, N'ASP.NET MVC Cookbook', CAST(38.00 AS Decimal(8, 2)), 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Precio], [CategoriaId]) VALUES (5, N'Avatar 3D', CAST(41.99 AS Decimal(8, 2)), 2)
INSERT [dbo].[Productos] ([Id], [Nombre], [Precio], [CategoriaId]) VALUES (8, N'The White Album - The Beatles', CAST(18.65 AS Decimal(8, 2)), 2)
INSERT [dbo].[Productos] ([Id], [Nombre], [Precio], [CategoriaId]) VALUES (9, N'Black BMX Bike', CAST(156.77 AS Decimal(8, 2)), 3)
SET IDENTITY_INSERT [dbo].[Productos] OFF

