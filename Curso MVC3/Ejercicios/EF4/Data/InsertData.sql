USE [TestDB]
GO

SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT [dbo].[Departments] ([Id], [Name]) VALUES (1, N'Human Resources')
INSERT [dbo].[Departments] ([Id], [Name]) VALUES (2, N'Sales')
INSERT [dbo].[Departments] ([Id], [Name]) VALUES (3, N'Information Technology')
INSERT [dbo].[Departments] ([Id], [Name]) VALUES (4, N'Production')
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO

SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT [dbo].[Employees] ([Id], [Name], [Age], [DepartmentId]) VALUES (1, N'Alberto Ezquieros', 35, NULL)
INSERT [dbo].[Employees] ([Id], [Name], [Age], [DepartmentId]) VALUES (2, N'Mark Sandoval', 25, 3)
INSERT [dbo].[Employees] ([Id], [Name], [Age], [DepartmentId]) VALUES (4, N'Luis Cordova', 28, 2)
INSERT [dbo].[Employees] ([Id], [Name], [Age], [DepartmentId]) VALUES (5, N'Carlos Velarde', 38, 1)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO

INSERT [dbo].[Employees_Salesman] ([Commission], [Id]) VALUES (CAST(10.5 AS Decimal(18, 0)), 1)
INSERT [dbo].[Employees_Salesman] ([Commission], [Id]) VALUES (CAST(9.5 AS Decimal(18, 0)), 2)
INSERT [dbo].[Employees_Salesman] ([Commission], [Id]) VALUES (CAST(8 AS Decimal(18, 0)), 4)
INSERT [dbo].[Employees_Salesman] ([Commission], [Id]) VALUES (CAST(13 AS Decimal(18, 0)), 5)
GO