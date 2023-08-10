CREATE PROCEDURE [dbo].[UpdateAgeIfChanged]
	@UserEmail nvarchar(255),
	@UserAge int
AS
BEGIN
	IF (SELECT UserAge FROM dbo.Users WHERE UserEmail = @UserEmail) != @UserAge
	 BEGIN
		UPDATE dbo.Users SET UserAge = @UserAge WHERE UserEmail = @UserEmail;
	 END
END