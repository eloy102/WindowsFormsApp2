using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class Class1
    {
        //static string connection_string = @"Data Source = 192.168.0.103; Initial Catalog = Z_ISERC; Persist Security Info=True;User ID = sa; Password=0000";
        static string connection_string = @"Data Source = DESKTOP-H9A7N7B; Initial Catalog = Z_ISERC; Integrated Security = True";

        public List<inary_flats> read_inary_flats()
        {
            String Path = Application.StartupPath.ToString();
            FileStream file1 = new FileStream(Path + "\\inary flats n.sql", FileMode.Open);
            StreamReader reader = new StreamReader(file1);


            var sql = reader.ReadToEnd();
            file1.Close();

            using (SqlConnection sqlcon = new SqlConnection(connection_string))
            {
                sqlcon.Open();
                var result = sqlcon.Query<inary_flats>(sql.ToString()).ToList();
                return result;
            }
        }

        public List<GisFlats> read_gis_flats(string Fiasguid)
        {
            String Path = Application.StartupPath.ToString();
            FileStream file1 = new FileStream(Path + "\\gis flats.sql", FileMode.Open);
            StreamReader reader = new StreamReader(file1);

            var sql = reader.ReadToEnd();
            file1.Close();


            using (SqlConnection sqlcon = new SqlConnection(connection_string))
            {
                sqlcon.Open();
                var result = sqlcon.Query<GisFlats>(sql.ToString() + " and p.FiasHouseGuid = @FiasHouseGuid", new { FiasHouseGuid = Fiasguid }).ToList();
                return result;
            }
        }

        public void update_inary_flats(inary_flats inary_Flats)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection_string))
            {
                sqlcon.Open();
                sqlcon.Update<inary_flats>(inary_Flats);
            }
        }
    }
}
