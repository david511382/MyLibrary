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
        public static List<T> GetData(string connectStr, string sqlcmd)
        {
            using (SqlConnection sc = new SqlConnection(connectStr))
            {
                return sc.Query<T>(sqlcmd).ToList();
            }
        }

        /// <summary>
        /// async get data from connectStr by sqlcmd
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public static List<T> GetDataAsync(string connectStr, string sqlcmd)
        {
            using (SqlConnection sc = new SqlConnection(connectStr))
            {
                return sc.QueryAsync<T>(sqlcmd).Result.ToList();
            }
        }

        /// <summary>
        /// async get data from connectStr by sqlcmd
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="sqlcmd"></param>
        /// <param name="paramss"></param>
        /// <returns></returns>
        public static List<T> GetDataAsync(string connectStr, string sqlcmd, KeyValuePair<string, dynamic>[] paramss)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (KeyValuePair<string, dynamic> d in paramss)
                dynamicParameters.Add(d.Key, d.Value);

            using (SqlConnection sc = new SqlConnection(connectStr))
            {
                return sc.QueryAsync<T>(sqlcmd, dynamicParameters).Result.ToList();
            }
        }
    }

    public static class DbQuery
    {
        /// <summary>
        /// async exc connectStr by sqlcmd
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="sqlcmd"></param>
        /// <param name="paramss"></param>
        /// <returns></returns>
        public static void ExcAsync(string connectStr, string sqlcmd, KeyValuePair<string, dynamic>[] paramss = null)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            if (paramss != null)
                foreach (KeyValuePair<string, dynamic> d in paramss)
                    dynamicParameters.Add(d.Key, d.Value);

            using (SqlConnection sc = new SqlConnection(connectStr))
            {
                if (paramss != null)
                    sc.Execute(sqlcmd, dynamicParameters);
                else
                    sc.Execute(sqlcmd);
            }
        }
    }
}
