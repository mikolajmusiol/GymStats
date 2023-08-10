CREATE TABLE [dbo].[Entries] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [UserId]       INT      NULL,
    [EntryDate]    DATETIME DEFAULT (NULL) NULL,
    [EntryTimeIn]  DATETIME DEFAULT (NULL) NULL,
    [EntryTimeOut] DATETIME DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Id] ASC)
);