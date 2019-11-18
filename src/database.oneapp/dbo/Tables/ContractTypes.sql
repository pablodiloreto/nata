CREATE TABLE [dbo].[ContractTypes] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [HoursRequired] BIT           NOT NULL,
    [Status]        BIT           NOT NULL,
    CONSTRAINT [PK_ContractTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

