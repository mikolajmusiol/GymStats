CREATE PROCEDURE [dbo].[GetEntries]
	@UserEmail nvarchar(255)
AS
BEGIN
	DECLARE @UserId INT
	SET @UserId = (SELECT Id FROM dbo.Users WHERE UserEmail = @UserEmail)

	SELECT * FROM dbo.Entries WHERE UserId = @UserId
END