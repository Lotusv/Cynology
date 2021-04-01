
declare @pedid int;
set @pedid=4;
WITH cte(pedigree_id, AgeLevel, Collis, ParId) AS
	(
		SELECT pedigree_id, 1 as AgeLevel, 0 as collis, 0 FROM tbl_pedigree AS B WHERE B.sire_name_id= @pedid OR B.dam_name_id= @pedid
		UNION ALL
		SELECT B.pedigree_id, cte.AgeLevel+1, Case when B.pedigree_id=@pedid then 1 else 0 end as collis, B.sire_name_id FROM tbl_pedigree AS B
			INNER JOIN cte ON cte.pedigree_id = B.sire_name_id 
				UNION ALL
		SELECT B.pedigree_id, cte.AgeLevel+1, Case when B.pedigree_id=@pedid then 1 else 0 end as collis, B.dam_name_id FROM tbl_pedigree AS B
			INNER JOIN cte ON cte.pedigree_id = B.dam_name_id 

	)

    SELECT  *
    FROM    cte 
    OPTION (MAXRECURSION 0);  