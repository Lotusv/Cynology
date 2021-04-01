use kino

go
update  dbo.tbl_pedigree set sire_name_id=null
go

update dbo.tbl_pedigree set sire_name_id = (SELECT top 1    t2.pedigree_id
FROM         dbo.dogs_forimport INNER JOIN
                      dbo.tbl_pedigree AS t1 ON dbo.dogs_forimport.ID = t1.zcode INNER JOIN
                      dbo.tbl_pedigree AS t2 ON dbo.dogs_forimport.Father = t2.zcode
WHERE     (t1.pedigree_id = dbo.tbl_pedigree.pedigree_id))