USE [master]
GO

SET NOCOUNT ON
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE [Name] = 'PayrollDb')
BEGIN
	ALTER DATABASE PayrollDb SET SINGLE_USER
	DROP DATABASE PayrollDb
END

CREATE DATABASE PayrollDb
GO

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE [name] = 'PayrollDbUser')
BEGIN
	CREATE LOGIN PayrollDbUser WITH PASSWORD = N'Password123', DEFAULT_DATABASE = PayrollDb,
		DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF
	
	ALTER LOGIN PayrollDbUser ENABLE
END
GO

USE PayrollDb
GO

CREATE USER PayrollDbUser FOR LOGIN PayrollDbUser
GO

EXEC sp_addrolemember N'db_datareader', N'PayrollDbUser'
EXEC sp_addrolemember N'db_datawriter', N'PayrollDbUser'
EXEC sp_addrolemember N'db_ddladmin', N'PayrollDbUser'
GO
