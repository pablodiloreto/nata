CREATE TABLE [dbo].[TicketUrgencies] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Weight] TINYINT      NOT NULL,
    [Name]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TicketUrgenty] PRIMARY KEY CLUSTERED ([Id] ASC)
);

