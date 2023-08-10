CREATE PROCEDURE [dbo].[AddEntries]
	@UserEmail nvarchar(255),
	@EntryDate datetime,
	@EntryTimeIn datetime,
	@EntryTimeOut datetime
AS
BEGIN
	DECLARE @UserId INT
	SET @UserId = (SELECT Id FROM dbo.Users WHERE UserEmail = @UserEmail)

	INSERT INTO dbo.Entries (UserId, EntryDate, EntryTimeIn, EntryTimeOut) VALUES (@UserId, @EntryDate, @EntryTimeIn, @EntryTimeOut)
END