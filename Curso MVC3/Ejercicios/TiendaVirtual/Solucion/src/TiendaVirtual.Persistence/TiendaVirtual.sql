
SET IDENTITY_INSERT dbo.Productos ON
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(2, N'Hola Mundo! Computación para niños', 25.50, 1, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(4, N'ASP.NET MVC Cookbook', 38.00, 1, N'mvc.jpg', N'image/jpeg')
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(5, N'Avatar 3D', 42.99, 2, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(8, N'The White Album - The Beatles', 18.65, 2, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(9, N'Black BMX Bike', 156.77, 3, N'bike.jpg', N'image/jpeg')
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(10, N'Smallville All Episodes Delete', 70.00, 1, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(12, N'Nuevo Producto', 100.00, 1, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(13, N'Otro Producto', 100.00, 2, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(14, N'Producto 2', 50.00, 3, NULL, NULL)
GO
INSERT INTO dbo.Productos(Id, Nombre, Precio, CategoriaId, Imagen_Nombre, Imagen_Tipo)
  VALUES(15, N'Producto 3', 100.00, 2, NULL, NULL)
GO
SET IDENTITY_INSERT dbo.Productos OFF
GO

