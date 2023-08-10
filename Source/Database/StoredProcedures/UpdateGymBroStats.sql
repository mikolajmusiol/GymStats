CREATE PROCEDURE [dbo].[UpdateGymBroStats]
	@UserEmail nvarchar(255), 
	@UserAvgTimeSpent time(0), 
	@UserAvgTimeIn time(0),
	@UserAvgTimeOut time(0),
	@UserTotalHoursSpent int,
	@UserTotalEntries int,
	@UserTotalMoneySpent int,
	@UserFirstWorkoutDate date,
	@UserAvgWorkoutsWeekly int,
	@UserLastLoginDate datetime
AS
BEGIN
	DECLARE @UserId INT
	SET @UserId = (SELECT Id FROM dbo.Users WHERE UserEmail = @UserEmail)

	IF EXISTS(SELECT 1 FROM dbo.UserStats WHERE UserId = @UserId)
	 BEGIN
		UPDATE dbo.UserStats SET 
		UserId = @UserId, 
		UserAvgTimeSpent = @UserAvgTimeSpent, 
		UserAvgTimeIn = @UserAvgTimeIn,
		UserAvgTimeOut = @UserAvgTimeOut,
		UserTotalHoursSpent = @UserTotalHoursSpent,
		UserTotalEntries = @UserTotalEntries,
		UserTotalMoneySpent = @UserTotalMoneySpent,
		UserFirstWorkoutDate = @UserFirstWorkoutDate,
		UserAvgWorkoutsWeekly = @UserAvgWorkoutsWeekly,
		UserLastLoginDate = @UserLastLoginDate
		WHERE UserId = @UserId
	 END
	ELSE
	 BEGIN
		INSERT INTO dbo.UserStats 
		(UserId, UserAvgTimeSpent, UserAvgTimeIn, 
		UserAvgTimeOut, UserTotalHoursSpent, UserTotalEntries, UserTotalMoneySpent, 
		UserFirstWorkoutDate, UserAvgWorkoutsWeekly, UserLastLoginDate) 
		VALUES (@UserId, @UserAvgTimeSpent, @UserAvgTimeIn, @UserAvgTimeOut,
		@UserTotalHoursSpent, @UserTotalEntries, @UserTotalMoneySpent, @UserFirstWorkoutDate,
		@UserAvgWorkoutsWeekly, @UserLastLoginDate);
	 END
END