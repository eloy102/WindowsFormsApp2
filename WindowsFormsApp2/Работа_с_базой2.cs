using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public class Работа_с_базой2
    {
        //static string connection_string = @"Data Source = 192.168.0.103; Initial Catalog = Z_ISERC; Persist Security Info=True;User ID = sa; Password=0000";
        static string connection_string = @"Data Source = DESKTOP-H9A7N7B; Initial Catalog = Z_ISERC; Integrated Security = True";
        public List<Buildings> read_all_buldings(int orgid, int termid)
        {
            using (SqlConnection sql_con = new SqlConnection(connection_string))
            {
                sql_con.OpenAsync();
                var buildings = sql_con.Query<Buildings>("select_all_buildings", new { org_id = orgid, term_id = termid }, commandType: CommandType.StoredProcedure);
                return buildings.ToList();
            }
        }

        public async Task<List<Buildings>> ReadAllBuldingsAsync(int orgid, int termid)
        {
            using (SqlConnection sql_con = new SqlConnection(connection_string))
            {
                await sql_con.OpenAsync();
                var buildings = sql_con.QueryAsync<Buildings>("select_all_buildings", new { org_id = orgid, term_id = termid }, commandType: CommandType.StoredProcedure).Result.ToList();
                return buildings.ToList();
            }
        }

        public async Task<List<inary_Flats1>> ReadFlatsAsync(int bldnid, int termid)
        {
            using (SqlConnection sql_con = new SqlConnection(connection_string))
            {
                await sql_con.OpenAsync();
                var flats = sql_con.QueryAsync<inary_Flats1>("select_flats1", new { bldn_id = bldnid, term_id = termid }, commandType: CommandType.StoredProcedure).Result.ToList();
                return flats;
            }
        }

        public async Task<List<occupations>> ReadOccAsync(int flatid, int bldnid, int termid)
        {
            using (SqlConnection sql_con = new SqlConnection(connection_string))
            {
                await sql_con.OpenAsync();
                var occ = sql_con.QueryAsync<occupations>("select_occupations", new { flat_id = flatid, bldn_id = bldnid, term_id = termid }, commandType: CommandType.StoredProcedure).Result.ToList();
                return occ;
            }
        }

        public async Task<List<Org>> ReadOrgAsync()
        {
            using (SqlConnection sql_con = new SqlConnection(connection_string))
            {
                await sql_con.OpenAsync();
                var org = sql_con.GetAllAsync<Org>().Result.ToList();
                return org;
            }
        }

        public async Task<List<GisFlats>> read_gis_flats(string Fiasguid)
        {
            String Path = Application.StartupPath.ToString();
            FileStream file1 = new FileStream(Path + "\\gis flats.sql", FileMode.Open);
            StreamReader reader = new StreamReader(file1);

            var sql = reader.ReadToEnd();
            file1.Close();


            using (SqlConnection sqlcon = new SqlConnection(connection_string))
            {
                await sqlcon.OpenAsync();
                var result = sqlcon.QueryAsync<GisFlats>(sql.ToString() + " and p.FiasHouseGuid = @FiasHouseGuid", new { FiasHouseGuid = Fiasguid }).Result.ToList();
                return result;
            }
        }
    }
}
