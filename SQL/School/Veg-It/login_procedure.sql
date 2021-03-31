CREATE PROCEDURE dbo.spLogin
    @pemail VARCHAR(50),
    @ppassword VARCHAR(50),
    @responseMessage VARCHAR(250)='' OUTPUT,
	@successVal int OUTPUT,
	@userId int OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @id INT

    IF EXISTS (SELECT TOP 1 id FROM [dbo].[users] WHERE email=@pemail)
    BEGIN
        SET @id=(SELECT id FROM [dbo].[users] WHERE email=@pemail AND passwordHash=HASHBYTES('SHA2_512', @ppassword+CAST(salt AS VARCHAR(36))))

       IF(@id IS NULL)
		BEGIN
           SET @responseMessage='Incorrect email or password'
		   SET @successVal = 0
		   SET @userId = -1
		END
       ELSE 
		BEGIN
           SET @responseMessage='Successfully logged in'
		   SET @successVal = 1
		   SET @userId = (SELECT TOP 1 id FROM [dbo].[users] WHERE email=@pemail)
		END
    END
    ELSE
		BEGIN
           SET @responseMessage='User with that email does not exist'
		   SET @successVal = 0
		   SET @userId = -1
		END

END
