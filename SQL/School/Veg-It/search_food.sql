CREATE PROCEDURE dbo.spSearchList
    @searchList VARCHAR(5000), 
    @searchSplit VARCHAR(5000),
	@size int
AS
BEGIN
	DROP TABLE IF EXISTS #temp
	DROP TABLE IF EXISTS #data

	SELECT TOP(100) F.ID, F.NAME, F.DIET_TYPE, D.NAME AS DIET_NAME, D.LEVEL AS DIET_LEVEL, F.DESCRIPTION, F.[GROUP], F.SUB_GROUP, F.DATA_SOURCE
	INTO #temp FROM FOODS F 
	INNER JOIN DIET_TYPES D ON(F.DIET_TYPE = D.ID)
	WHERE CONTAINS(F.Name, @searchList) 
	ORDER BY
		F.DATA_SOURCE desc

	CREATE TABLE #data(
		id int,
		name varchar(255) NOT NULL,
		diet_type int NOT NULL,
		diet_name varchar(50) NOT NULL,
		diet_level int NOT NULL,
		description varchar(5000) NULL,
		data_source int NOT NULL,
	)

	DECLARE @count INT = 1
	DECLARE @searchTerm VARCHAR(255)

	WHILE @count <= @size
	BEGIN
		SET @searchTerm = (SELECT CAST('<t>' + REPLACE(@searchSplit, ',','</t><t>') + '</t>' AS XML).value('/t[sql:variable("@count")][1]','varchar(255)'))

		IF EXISTS(select * from #temp where name = @searchTerm)
		BEGIN
			INSERT INTO #data (id, name, diet_type, diet_name, diet_level, description, data_source) 
			SELECT TOP(1) id, name, diet_type, diet_name, diet_level, description, data_source 
			FROM #temp WHERE name = @searchTerm
		END
		ELSE
		BEGIN
			INSERT INTO #data (id, name, diet_type, diet_name, diet_level, description, data_source) 
			SELECT TOP(1) id, name, diet_type, diet_name, diet_level, description, data_source 
			FROM #temp WHERE name LIKE '%' + @searchTerm + '%'
		END
	
		-- nothing found, add as blank
		IF NOT EXISTS(select * from #data where name = @searchTerm)
		BEGIN
			INSERT INTO #data (id, name, diet_type, diet_name, diet_level, description, data_source) 
			VALUES (-1, @searchTerm, 5, 'Unspecified', -1, null, -1)
		END

		SET @count = @count + 1;
	END;

	SELECT * FROM #data

	DROP TABLE #temp
	DROP TABLE #data
END
