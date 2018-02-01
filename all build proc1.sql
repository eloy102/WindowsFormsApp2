use Z_ISERC;
go
create procedure select_all_buildings(@org_id int, @term_id int)
 as SELECT b.Address, b.bldn_id,b.FiasGuid,b.org_id
 FROM            INARY_BUILDINGS b
where 
b.org_id = @org_id
and b.term_id = @term_id
order by b.bldn_id