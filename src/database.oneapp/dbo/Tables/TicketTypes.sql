CREATE TABLE [dbo].[TicketTypes] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TicketTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

