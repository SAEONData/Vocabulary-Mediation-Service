--Hazard Family--
IF((SELECT COUNT(*) FROM Hazards) = 0)
BEGIN
	INSERT INTO
		Hazards
		(
			[Value],
			HazardTypeId
		)
	SELECT DISTINCT
		H.[Hazard Family],
		1
	FROM
		[Hazards - Sendai] H
	ORDER BY
		H.[Hazard Family]

	--Sub Family--
	INSERT INTO
		Hazards
		(
			[Value],
			HazardTypeId,
			ParentId
		)	
	SELECT DISTINCT
		H.[Sub-Family],
		2,
		T.Id
	FROM
		[Hazards - Sendai] H
	INNER JOIN
		Hazards T
		ON T.Value = H.[Hazard Family]		
		AND T.HazardTypeId = 1
	ORDER BY
		H.[Sub-Family]

	--Hazard--
	INSERT INTO
		Hazards
		(
			[Value],
			HazardTypeId,
			ParentId
		)	
	SELECT DISTINCT
		H.Hazard,
		3,
		T.Id
	FROM
		[Hazards - Sendai] H
	INNER JOIN
		Hazards T
		ON T.Value = H.[Sub-Family]
		AND T.HazardTypeId = 2
	ORDER BY
		h.Hazard

	--Sub Hazard--
	INSERT INTO
		Hazards
		(
			[Value],
			HazardTypeId,
			ParentId
		)	
	SELECT DISTINCT
		H.[Sub-Hazard],
		4,
		T.Id
	FROM
		[Hazards - Sendai] H
	INNER JOIN
		Hazards T
		ON T.Value = H.Hazard
		AND T.HazardTypeId = 3
	WHERE
		H.[Sub-Hazard] <> ''
	ORDER BY
		H.[Sub-Hazard]
END

--SELECT--
SELECT
	T1.Value AS HazardFamily,
	T2.Value AS SubFamily,
	T3.Value AS Hazard,
	T4.Value AS SubHazard
FROM
	Hazards T1
LEFT OUTER JOIN
	(
		SELECT	
			*
		FROM
			Hazards
		WHERE
			HazardTypeId = 2
	) T2
	ON T2.ParentId = T1.Id 
LEFT OUTER JOIN
	(
		SELECT
			*
		FROM
			Hazards
		WHERE
			HazardTypeId = 3
	) T3
	ON T3.ParentId = T2.Id
LEFT OUTER JOIN
	(
		SELECT
			*
		FROM
			Hazards
		WHERE
			HazardTypeId = 4
	) T4
	ON T4.ParentId = T3.Id
WHERE
	T1.HazardTypeId = 1
ORDER BY
	T1.Value,
	T2.Value,
	T3.Value,
	T4.Value