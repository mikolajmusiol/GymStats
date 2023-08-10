CREATE TABLE [dbo].[Users] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [UserEmail]     NVARCHAR (255) NOT NULL,
    [UserName]      NVARCHAR (255) NOT NULL,
    [UserSurname]   NVARCHAR (255) NOT NULL,
    [UserAge]       INT            NOT NULL,
    [UserPassPrice] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Id] ASC)
);