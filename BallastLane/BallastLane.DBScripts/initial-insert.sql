-- Insert initial data into the Users table
USE [BallastLane]
GO
INSERT INTO [dbo].[Users]([Name],[email], [loginname], [password])
     VALUES
           ('Administrator', 'adm@BallastLane.com', 'adm', 'password1');
GO

-- Insert initial data into the Products table
INSERT INTO [dbo].[Products] ([Name], [Price], [Description])
    VALUES
        ('Product 1', 19.99, 'Description of Product 1'),
        ('Product 2', 29.99, 'Description of Product 2'),
        ('Product 3', 39.99, 'Description of Product 3');
