CREATE TABLE [dbo].[Accounts] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [Address]   VARCHAR (50) NOT NULL,
    [Phone]     VARCHAR (50) NOT NULL,
    [CountryId] INT          NOT NULL,
    [Email]     VARCHAR (50) NOT NULL,
    [Status]    BIT          NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Accounts_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
);

