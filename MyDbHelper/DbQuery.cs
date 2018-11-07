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
                        DynamicParameters dynamicParameters = DbQuery.GetDynamicParametersWithKeyValuePairs(paramss);

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
                        DynamicParameters dynamicParameters = DbQuery.GetDynamicParametersWithKeyValuePairs(paramss);

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
        public static async Task ExcAsync(string connectStr, string sqlcmd,params KeyValuePair<string, dynamic>[] paramss)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                if (paramss != null)
                    dynamicParameters = DbQuery.GetDynamicParametersWithKeyValuePairs(paramss);

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

        public static void Exc(string connectStr, string sqlcmd, params KeyValuePair<string, dynamic>[] paramss)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                if (paramss != null)
                    dynamicParameters = GetDynamicParametersWithKeyValuePairs(paramss);

                using (SqlConnection sc = new SqlConnection(connectStr))
                {
                    if (paramss != null)
                        sc.Execute(sqlcmd, dynamicParameters);
                    else
                        sc.Execute(sqlcmd);
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

        internal static DynamicParameters GetDynamicParametersWithKeyValuePairs(KeyValuePair<string, dynamic>[] keyValuePairs)
        {
            if (keyValuePairs == null)
                return new DynamicParameters();

            DynamicParameters result = new DynamicParameters();
            DateTime dateTime;
            string valueStr;
            foreach (KeyValuePair<string, dynamic> d in keyValuePairs)
            {
                valueStr = Convert.ToString(d.Value);

                if (CanParseDateTime(valueStr) && DateTime.TryParse(valueStr, out dateTime))
                    result.Add(d.Key, dateTime);
                else
                    result.Add(d.Key, d.Value);
            }

            return result;
        }

        private static bool CanParseDateTime(string dateStr)
        {
            string[] dateStrPart = dateStr.Split(' ');
            try
            {
                if (
                    dateStrPart[0][4] == '/' &&
                    (dateStrPart[0][6] == '/' || dateStrPart[0][7] == '/') &&
                    dateStrPart[1].Contains("午") &&
                    dateStrPart[2][2] == ':' &&
                    dateStrPart[2][5] == ':'
                    )
                    return true;
            }
            catch { }
            return false;
        }
    }
}
