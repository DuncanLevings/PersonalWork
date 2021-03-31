CREATE PROCEDURE dbo.spAddFood
    @pname VARCHAR(255), 
    @pdiet_type INT, 
    @pdescription VARCHAR(5000) = NULL, 
    @ppublic_id VARCHAR(50) = NULL,
	@ppublic_id_int INT = NULL,
	@pgroup VARCHAR(50) = NULL,
	@psub_group VARCHAR(50) = NULL,
	@pcreated_date VARCHAR(50),
	@pupdated_date VARCHAR(50),
	@pdata_source INT,
    @responseMessage VARCHAR(250) OUTPUT,
	@successVal int OUTPUT,
	@foodID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		IF EXISTS (SELECT TOP 1 id FROM [dbo].[foods] WHERE name=@pname)
		BEGIN
			SET @responseMessage='Ingredient with that name already exists'
			SET @successVal = 0
			SET @foodID = -1
		END
		ELSE
		BEGIN
			DECLARE @public_int INT

			IF (@ppublic_id_int = 0)
			BEGIN
				SET @public_int = (SELECT MIN(public_id_int) FROM foods)
				SET @public_int = @public_int - 1
			END
			ELSE
			BEGIN
				SET @public_int = @ppublic_id_int
			END

			INSERT INTO dbo.[foods] (name, diet_type, description, public_id, public_id_int, [group], sub_group, created_date, updated_date, data_source)
			VALUES(@pname, @pdiet_type, @pdescription, @ppublic_id, @public_int, @pgroup, @psub_group, @pcreated_date, @pupdated_date, @pdata_source)

			SET @responseMessage='Success'
			SET @successVal = 1
			SET @foodID = (SELECT TOP 1 id FROM [dbo].[foods] WHERE name=@pname)
		END
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
		set @successVal = 0
		SET @foodID = -1
    END CATCH

END
