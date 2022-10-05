CREATE TABLE [dbo].[Item] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [ProductName]  VARCHAR (100) NOT NULL,
    [ProductPrice] DECIMAL(18, 2) NOT NULL,
    [Quantity]  INT NOT NULL,
    [OrderID] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Item_ToOrder] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([Id])
);

