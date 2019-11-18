CREATE TABLE [dbo].[AccountContacts] (
    [AccountId] INT NOT NULL,
    [ContactId] INT NOT NULL,
    CONSTRAINT [PK_AccountContacts] PRIMARY KEY CLUSTERED ([AccountId] ASC, [ContactId] ASC),
    CONSTRAINT [FK_AccountContacts_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_AccountContacts_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id])
);

