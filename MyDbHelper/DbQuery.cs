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
        /// <summary>
        /// get data from connectStr by sqlcmd
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public static List<T> GetData(string sqlcmd, string connectStr)
        {
            using (SqlConnection sc = new SqlConnection(connectStr))
            {
                return sc.Query<T>(sqlcmd).ToList();
            }
        }
    }
}
