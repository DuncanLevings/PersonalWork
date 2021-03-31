CREATE PROCEDURE dbo.spAddUser
    @pemail VARCHAR(50), 
    @ppassword VARCHAR(50), 
    @pfirstName VARCHAR(50) = NULL, 
    @plastName VARCHAR(50) = NULL,
    @responseMessage VARCHAR(250) OUTPUT,
	@successVal int OUTPUT,
	@userId int OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY
		IF EXISTS (SELECT TOP 1 id FROM [dbo].[users] WHERE email=@pemail)
		BEGIN
			SET @responseMessage='User with that email already exists'
			SET @successVal = 0
			SET @userId = -1
		END
		ELSE
		BEGIN
			INSERT INTO dbo.[users] (email, passwordHash, firstName, lastName, salt)
			VALUES(@pemail, HASHBYTES('SHA2_512', @ppassword+CAST(@salt as VARCHAR(36))), @pfirstName, @plastName, @salt)

			SET @responseMessage='Success'
			SET @successVal = 1
			SET @userId = (SELECT TOP 1 id FROM [dbo].[users] WHERE email=@pemail)
		END
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
		set @successVal = 0
		SET @userId = -1
    END CATCH

END
