CREATE TABLE [dbo].[Contracts] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (50) NOT NULL,
    [AccountId]      INT          NOT NULL,
    [ContractTypeId] INT          NOT NULL,
    [DateFrom]       DATE         NOT NULL,
    [DateTo]         DATE         NOT NULL,
    [Hours]          SMALLINT     NOT NULL,
    [Status]         BIT          NOT NULL,
    CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contracts_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Contracts_ContractTypes] FOREIGN KEY ([ContractTypeId]) REFERENCES [dbo].[ContractTypes] ([Id])
);

