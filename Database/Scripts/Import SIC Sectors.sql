DECLARE @tmp TABLE
(
	Id int identity(1,1),
	ParentId int,
	SectorTypeId int not null,
	[Value] nvarchar(max) not null,
	SIC_Code nvarchar(max)
)

--Division--
INSERT INTO
	@tmp
	(
		[Value],
		SIC_Code,
		SectorTypeId
	)
SELECT DISTINCT
	S.SIC_Division_Name,
	S.SIC_Division,
	1
FROM
	SIC_Sectors S
ORDER BY
	S.SIC_Division_Name

--MajorGroup--
INSERT INTO
	@tmp
	(
		[Value],
		SIC_Code,
		SectorTypeId,
		ParentId
	)
SELECT DISTINCT
	S.SIC_Major_Group_Name,
	S.SIC_Major_Group,
	2,
	T.Id
FROM
	SIC_Sectors S
INNER JOIN
	@tmp T
	ON T.Value = S.SIC_Division_Name
	AND T.SectorTypeId = 1
ORDER BY
	S.SIC_Major_Group_Name

--Sector--
INSERT INTO
	@tmp
	(
		[Value],
		SIC_Code,
		SectorTypeId,
		ParentId
	)
SELECT DISTINCT
	LTRIM(RTRIM(RIGHT(S.[SIC 3], LEN(S.[SIC 3]) -  CHARINDEX(' - ', S.[SIC 3]) - 1))),
	LTRIM(RTRIM(LEFT(S.[SIC 3], CHARINDEX(' - ', S.[SIC 3])))),
	3,
	T.Id
FROM
	SIC_Sectors S
INNER JOIN
	@tmp T
	ON T.Value = S.SIC_Major_Group_Name
	AND T.SectorTypeId = 2
ORDER BY
	LTRIM(RTRIM(RIGHT(S.[SIC 3], LEN(S.[SIC 3]) -  CHARINDEX(' - ', S.[SIC 3]) - 1)))

IF((SELECT COUNT(*) FROM Sectors) = 0)
BEGIN
	INSERT INTO
		Sectors
		(
			[Value],
			SIC_Code,
			SectorTypeId,
			ParentId
		)
	SELECT
		T.[Value],
		T.SIC_Code,
		T.SectorTypeId,
		T.ParentId
	FROM
		@tmp T
END

SELECT
	*
FROM
	Sectors