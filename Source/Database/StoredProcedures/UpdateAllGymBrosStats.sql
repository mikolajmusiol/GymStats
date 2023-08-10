CREATE PROCEDURE [dbo].[UpdateAllGymBrosStats]
	@AllAvgTimeSpent time(0),
	@AllAvgTimeIn time(0),
	@AllAvgTimeOut time(0),
	@AllAvgAge int,
	@AllAvgPassPrice int,
	@AllAvgWorkoutsWeekly int,
	@AllAvgTotalHours int,
	@AllAvgMoneySpent int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.AllStats WHERE Id = 1)
	 BEGIN
		UPDATE dbo.AllStats SET 
		AllAvgTimeSpent = @AllAvgTimeSpent, 
		AllAvgTimeIn = @AllAvgTimeIn,
		AllAvgTimeOut = @AllAvgTimeOut,
		AllAvgAge = @AllAvgAge,
		AllAvgPassPrice = @AllAvgPassPrice,
		AllAvgWorkoutsWeekly = @AllAvgWorkoutsWeekly,
		AllAvgTotalHours = @AllAvgTotalHours,
		AllAvgMoneySpent = @AllAvgMoneySpent
		WHERE Id = 1
	 END
	ELSE
	 BEGIN
		SET IDENTITY_INSERT dbo.AllStats ON
		INSERT INTO dbo.AllStats 
		(Id, AllAvgTimeSpent, AllAvgTimeIn, 
		AllAvgTimeOut, AllAvgAge, AllAvgPassPrice, AllAvgWorkoutsWeekly, 
		AllAvgTotalHours, AllAvgMoneySpent) 
		VALUES (1, @AllAvgTimeSpent, @AllAvgTimeIn, @AllAvgTimeOut,
		@AllAvgAge, @AllAvgPassPrice, @AllAvgWorkoutsWeekly, @AllAvgTotalHours,
		@AllAvgMoneySpent);
		SET IDENTITY_INSERT dbo.AllStats OFF
	 END
END