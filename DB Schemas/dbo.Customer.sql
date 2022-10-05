CREATE TABLE [dbo].[Customer] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Firstname]  VARCHAR (50)  NOT NULL,
    [Lastname]   VARCHAR (50)  NOT NULL,
    [Address]    VARCHAR (150) NOT NULL,
    [PostalCode] VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

