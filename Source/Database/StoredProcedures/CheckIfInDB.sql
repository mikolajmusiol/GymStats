CREATE PROCEDURE [dbo].[CheckIfInDB]
	@UserEmail nvarchar(255)
AS
BEGIN
	SELECT COUNT(1) FROM dbo.Users WHERE UserEmail = @UserEmail
END