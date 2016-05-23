/*
Deployment script for TiendaVirtual
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TiendaVirtual"
:setvar DefaultDataPath "D:\Services\MSSQL\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Services\MSSQL\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target WIN7X64NB. The database for which this script was built, TiendaVirtual, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'WIN7X64NB')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'WIN7X64NB',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]

GO
/*
The type for column Precio in table [dbo].[Productos] is currently  DECIMAL (10, 2) NOT NULL but is being changed to  DECIMAL (8, 2) NOT NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Productos])
    RAISERROR ('Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping FK_CategoriaProducto...';


GO
ALTER TABLE [dbo].[Productos] DROP CONSTRAINT [FK_CategoriaProducto];


GO
PRINT N'Starting rebuilding table [dbo].[Productos]...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_Productos] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]        NVARCHAR (100) NOT NULL,
    [Precio]        DECIMAL (8, 2) NOT NULL,
    [CategoriaId]   INT            NOT NULL,
    [Imagen_Nombre] NVARCHAR (50)  NULL,
    [Imagen_Tipo]   NVARCHAR (50)  NULL
);

ALTER TABLE [dbo].[tmp_ms_xx_Productos]
    ADD CONSTRAINT [tmp_ms_xx_clusteredindex_PK_Productos] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[Productos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Productos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Productos] ([Id], [Nombre], [Precio], [CategoriaId], [Imagen_Nombre], [Imagen_Tipo])
        SELECT   [Id],
                 [Nombre],
                 CAST ([Precio] AS DECIMAL (8, 2)),
                 [CategoriaId],
                 [Imagen_Nombre],
                 [Imagen_Tipo]
        FROM     [dbo].[Productos]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Productos] OFF;
    END

DROP TABLE [dbo].[Productos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Productos]', N'Productos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_clusteredindex_PK_Productos]', N'PK_Productos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'Creating [dbo].[Productos].[IX_FK_CategoriaProducto]...';


GO
CREATE NONCLUSTERED INDEX [IX_FK_CategoriaProducto]
    ON [dbo].[Productos]([CategoriaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating FK_CategoriaProducto...';


GO
ALTER TABLE [dbo].[Productos] WITH NOCHECK
    ADD CONSTRAINT [FK_CategoriaProducto] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categorias] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Productos] WITH CHECK CHECK CONSTRAINT [FK_CategoriaProducto];


GO
