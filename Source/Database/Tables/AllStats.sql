CREATE TABLE [dbo].[AllStats] (
    [Id]                   INT      IDENTITY (1, 1) NOT NULL,
    [AllAvgTimeSpent]      TIME (0) NOT NULL,
    [AllAvgTimeIn]         TIME (0) NOT NULL,
    [AllAvgTimeOut]        TIME (0) NOT NULL,
    [AllAvgAge]            INT      NOT NULL,
    [AllAvgPassPrice]      INT      NOT NULL,
    [AllAvgWorkoutsWeekly] INT      NOT NULL,
    [AllAvgTotalHours]     INT      NOT NULL,
    [AllAvgMoneySpent]     INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Id] ASC)
);