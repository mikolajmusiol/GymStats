CREATE TABLE [dbo].[UserStats] (
    [Id]                    INT      IDENTITY (1, 1) NOT NULL,
    [UserId]                INT      NOT NULL,
    [UserAvgTimeSpent]      TIME (0) NOT NULL,
    [UserAvgTimeIn]         TIME (0) NOT NULL,
    [UserAvgTimeOut]        TIME (0) NOT NULL,
    [UserTotalHoursSpent]   INT      NOT NULL,
    [UserTotalEntries]      INT      NOT NULL,
    [UserTotalMoneySpent]   INT      NOT NULL,
    [UserFirstWorkoutDate]  DATE     NULL,
    [UserAvgWorkoutsWeekly] INT      NOT NULL,
    [UserLastLoginDate]     DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Id] ASC)
);
