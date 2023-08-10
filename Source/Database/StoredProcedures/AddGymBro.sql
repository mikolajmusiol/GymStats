CREATE PROCEDURE [dbo].[AddGymBro]
	@UserEmail nvarChar(255),
	@UserName nvarChar(255),
	@UserSurname nvarChar(255),
	@UserAge int,
	@UserPassPrice int
AS
BEGIN
	INSERT INTO dbo.Users (UserEmail, UserName, UserSurname, UserAge, UserPassPrice) VALUES (@UserEmail, @UserName, @UserSurname, @UserAge, @UserPassPrice)
END