using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace WindowsFormsApp2
{
    public class GisFlats
    {
        [ExplicitKey]
        public string AccountNumber { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string PremisesNum { get; set; }   
        public string FiasHouseGuid { get; set; }
        public int? Living { get; set; }
        public string PremisesGUID { get; set; }
        public string PremisesUniqueNumber { get; set; }
        public decimal? TotalArea { get; set; }
        public DateTime? TerminationDate { get; set; }

    }
}
