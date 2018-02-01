using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
namespace WindowsFormsApp2
{
    [Table("Org")]
    public class Org
    {
        [ExplicitKey]
        public int id { get; set; }
        public string Name { get; set; }
        public string OGRN { get; set; }
        public string PPAGUID { get; set; }
        public string RootEntityGUID { get; set; }
    }
}
