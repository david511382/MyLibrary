using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDbHelper
{
    public static class DbQuery<T>
    {
        public static List<T> GetData(string sqlcmd)
        {
            using (SqlConnection sc = new SqlConnection(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=Mvc;Integrated Security=True"))
            {
                return sc.Query<T>(sqlcmd).ToList();
            }
        }
    }
}
