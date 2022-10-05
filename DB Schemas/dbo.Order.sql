CREATE TABLE [dbo].[Order] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [OrderDate]  DATE            NOT NULL,
    [TotalPrice] DECIMAL (18, 2) NOT NULL,
    [CustomerID] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_ToCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([Id])
);

