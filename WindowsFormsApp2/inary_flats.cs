using System;
using Dapper;
using Dapper.Contrib.Extensions;

namespace WindowsFormsApp2
{
    [Table("INARY_FLATS")]
    public class inary_flats
    {
        public string occ_name { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        [ExplicitKey]
        public string base_name_inary { get; set; }
        public int bldn_id { get; set; }
        public int flat_id { get; set; }
        public string FiasGuid { get; set; }
        public string flat_no { get; set; }
        public string flat_no2 { get; set; }
        public decimal? total_sq { get; set; }
        public bool? deserted { get; set; }
        public bool? closed { get; set; }
        public string roomtype_id { get; set; }
        public string PremisesGUID { get; set; }
        public string PremisesNum { get; set; }
        public string PremisesUniqueNumber { get; set; }
        

        public inary_flats()
        {

        }
    }

    
}
