use Z_ISERC;

declare 
	@org_id int,
	@term_id int

set @org_id = 1
set @term_id = 201712

SELECT TOP (1000) b.Address, b.bldn_id,b.FiasGuid, b.org_id
 FROM            INARY_BUILDINGS b
where 
b.org_id = @org_id
and b.term_id = @term_id
order by b.bldn_id