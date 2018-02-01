use Z_ISERC
go 

create procedure select_occupations(@flat_id int, @term_id int,@bldn_id int)
as
SELECT    occ.occ_name,occ.FIO,occ.AccountGuid,occ.PremisesGUID

FROM	INARY_BUILDINGS b inner join
		INARY_FLATS f on f.base_name_inary = b.base_name_inary and f.bldn_id = b.bldn_id inner join
		INARY_OCCUPATIONS occ on f.base_name_inary = occ.base_name_inary and occ.flat_id = f.flat_id

where occ.flat_id=@flat_id
and b.term_id = @term_id
and f.bldn_id = @bldn_id