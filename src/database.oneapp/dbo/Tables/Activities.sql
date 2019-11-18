CREATE TABLE [dbo].[Activities] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [TicketId] INT            NOT NULL,
    [Date]     DATE           NOT NULL,
    [UserId]   NVARCHAR (450) NOT NULL,
    [Efforts]  DECIMAL (18)   NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Activities_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Activities_Tickets1] FOREIGN KEY ([TicketId]) REFERENCES [dbo].[Tickets] ([Id])
);

