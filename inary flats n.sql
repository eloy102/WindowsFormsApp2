use Z_ISERC

declare 
	@org_id int,
	@term_id int

set @org_id = 1
set @term_id = 201712

SELECT   b.Address, f.flat_no, f.flat_no2, f.flat_id, f.FiasGuid
FROM            INARY_BUILDINGS b INNER JOIN
                         INARY_FLATS f ON f.FiasGuid = b.FiasGuid
							
							
						 
where 

b.org_id = @org_id
and b.term_id = @term_id
and f.PremisesGUID is null
and f.closed =0
and f.total_sq !=0