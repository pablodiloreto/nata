CREATE TABLE [dbo].[Contacts] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [Name]  VARCHAR (50) NOT NULL,
    [Phone] VARCHAR (50) NOT NULL,
    [Email] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

