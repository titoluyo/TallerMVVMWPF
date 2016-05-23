-- Enable FILESTREAM support
USE master;

EXEC sys.sp_configure N'filestream access level', N'2'
GO

RECONFIGURE WITH OVERRIDE
GO 


-- Create a new file group
ALTER DATABASE TiendaVirtual 
   ADD FILEGROUP FileStreamGroup CONTAINS FILESTREAM
go

-- Add a file to the file group, we can now use the file group to store data
ALTER DATABASE TiendaVirtual
ADD FILE
(
    NAME= 'TiendaVirtual_FileGroup',
    FILENAME = 'C:\temp\filestream\'
)
TO FILEGROUP FileStreamGroup
GO

