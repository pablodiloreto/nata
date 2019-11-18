CREATE TABLE [dbo].[TicketImpacts] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Weight] TINYINT      NOT NULL,
    [Name]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TicketImpacts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

