use Z_ISERC;

go

create procedure select_flats1(@bldn_id int,@term_id int)
as 
SELECT f.flat_id, f.flat_no,f.flat_no2,f.closed,f.roomtype_id,f.total_sq,f.PremisesGUID,f.PremisesNum,f.PremisesUniqueNumber

from INARY_BUILDINGS b INNER JOIN
       INARY_FLATS f ON f.base_name_inary = b.base_name_inary and f.bldn_id = b.bldn_id
	  
where 
f.bldn_id = @bldn_id
and b.term_id = @term_id
order by f.flat_id