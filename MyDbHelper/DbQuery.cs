using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <param name="connectStr"></param>
        /// <param name="sqlcmd"></param>
        /// <param name="paramss"></param>
        /// <returns></returns>
        public static List<T> GetData(string connectStr, string sqlcmd, params KeyValuePair<string, dynamic>[] paramss)
        {
            try
            {
                using (SqlConnection sc = new SqlConnection(connectStr))
                {
                    if (paramss != null)
                    {
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        foreach (KeyValuePair<string, dynamic> d in paramss)
                            dynamicParameters.Add(d.Key, d.Value);
                        return sc.Query<T>(sqlcmd, dynamicParameters).ToList();
                    }

                    return sc.Query<T>(sqlcmd).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// async get data from connectStr by sqlcmd
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="sqlcmd"></param>
        /// <param name="paramss"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetDataAsync(string connectStr, string sqlcmd, params KeyValuePair<string, dynamic>[] paramss)
        {
            try
            {
                using (SqlConnection sc = new SqlConnection(connectStr))
                {
                    IEnumerable<T> result;

                    if (paramss != null)
                    {
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        foreach (KeyValuePair<string, dynamic> d in paramss)
                            dynamicParameters.Add(d.Key, d.Value);
                        result = await sc.QueryAsync<T>(sqlcmd, dynamicParameters);
                        return result.ToList();
                    }

                    result = await sc.QueryAsync<T>(sqlcmd);
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public static class DbQuery
    {
        #region exc
        /// <summary>
        /// async exc connectStr by sqlcmd
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="sqlcmd"></param>
        /// <param name="paramss"></param>
        /// <returns></returns>
        public static async Task ExcAsync(string connectStr, string sqlcmd, KeyValuePair<string, dynamic>[] paramss = null)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                if (paramss != null)
                    foreach (KeyValuePair<string, dynamic> d in paramss)
                        dynamicParameters.Add(d.Key, d.Value);

                using (SqlConnection sc = new SqlConnection(connectStr))
                {
                    if (paramss != null)
                        await sc.ExecuteAsync(sqlcmd, dynamicParameters);
                    else
                        await sc.ExecuteAsync(sqlcmd);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region get
        public static DataTable GetData(string strSql, string strConn, params SqlParameter[] sqlParameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sc = new SqlConnection(strConn))
                {
                    using (SqlCommand sqlCom = new SqlCommand(strSql, sc))
                    {
                        sc.Open();
                        if (sqlParameters != null)
                            sqlCom.Parameters.AddRange(sqlParameters);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCom))
                        {
                            adapter.Fill(dt);
                            sqlCom.Dispose();
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion
    }
}
