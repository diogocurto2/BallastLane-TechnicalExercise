 --Create a new database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BallastLane')
BEGIN
	CREATE DATABASE [BallastLane];
END;
GO
	USE [BallastLane]
GO

-- Create the Users table
CREATE TABLE [Users] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(255) NOT NULL,
  [Email] NVARCHAR(255) NOT NULL,
  [LoginName] NVARCHAR(255) NOT NULL,
  [Password] NVARCHAR(255) NOT NULL
);

-- Create the Products table
CREATE TABLE [Products] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(255) NOT NULL,
  [Price] DECIMAL(18, 2) NOT NULL,
  [Description] NVARCHAR(1000) NOT NULL
);