CREATE TABLE [dbo].[Tickets] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (50)   NOT NULL,
    [ContractId]      INT            NOT NULL,
    [AssignedTo]      NVARCHAR (450) NULL,
    [Description]     VARCHAR (MAX)  NOT NULL,
    [DateFrom]        DATE           NOT NULL,
    [ContactId]       INT            NOT NULL,
    [TicketTypeId]    INT            NOT NULL,
    [EstimatedHours]  TINYINT        NULL,
    [CreatedBy]       NVARCHAR (450) NOT NULL,
    [TicketImpactId]  INT            NOT NULL,
    [TicketUrgencyId] INT            NOT NULL,
    [TicketPriority]  TINYINT        NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tickets_AspNetUsers] FOREIGN KEY ([AssignedTo]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Tickets_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id]),
    CONSTRAINT [FK_Tickets_Contracts] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contracts] ([Id]),
    CONSTRAINT [FK_Tickets_TicketImpacts] FOREIGN KEY ([TicketImpactId]) REFERENCES [dbo].[TicketImpacts] ([Id]),
    CONSTRAINT [FK_Tickets_TicketTypes] FOREIGN KEY ([TicketTypeId]) REFERENCES [dbo].[TicketTypes] ([Id]),
    CONSTRAINT [FK_Tickets_TicketUrgencies] FOREIGN KEY ([TicketUrgencyId]) REFERENCES [dbo].[TicketUrgencies] ([Id])
);

