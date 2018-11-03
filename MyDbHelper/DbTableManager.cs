using MyReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDbHelper
{
    public class NotInsertAble : Attribute
    {

    }

    public class NotDataColumn: Attribute
    {

    }

    public interface DbTable
    {
        string GetTableName();
    }

    public static class DbTableManager<T> where T : DbTable
    {
        public static void Insert(T tableStruct, string connectStr)
        {
            string[] colNames = GetInsertColNames(tableStruct);
            object[] values = Reflection.GetValuesByNames(tableStruct, colNames);
            string insertSql = GetInsertSqlcmd(tableStruct.GetTableName(), colNames);

            List<KeyValuePair<string, dynamic>> parameters = GetParameters(colNames, values); 

            DbQuery.Exc(connectStr, insertSql, parameters.ToArray());
        }

        public static void Insert(List<T> tableStructs, string connectStr)
        {
            if (tableStructs.Count == 0)
                return;

            string[] colNames = GetInsertColNames(tableStructs[0]);
            string insertSql = GetInsertSqlcmd(tableStructs[0].GetTableName(), colNames);
            object[] values;
            List<KeyValuePair<string, dynamic>> parameters;

            foreach (T obj in tableStructs)
            {
                values = Reflection.GetValuesByNames(obj, colNames);
                parameters = GetParameters(colNames, values);
                DbQuery.Exc(connectStr, insertSql, parameters.ToArray());
            }
        }

        private static List<KeyValuePair<string, dynamic>> GetParameters(string[] colNames ,object[] values)
        {
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            for (int i = 0; i < colNames.Length; i++)
            {
                parameters.Add(new KeyValuePair<string, dynamic>(colNames[i], values[i]));
            }
            return parameters;
        }

        private static string GetInsertSqlcmd(string tableName, string[] colNames)
        {
            string insertSql = "INSERT INTO " + tableName + " (";
            string insertSqlValue = "VALUES (";
            string s = ",";
            string colName;
            for (int i = 0; i < colNames.Length; i++)
            {
                if (i == colNames.Length - 1)
                    s = "";
                colName = colNames[i];
                insertSql += " " + colName + s;
                insertSqlValue += " @" + colName + s;
            }
            insertSql += " ) " + insertSqlValue + " ) ";
            return insertSql;
        }

        private static string[] GetInsertColNames(T target)
        {
            List<string> colNames = new List<string>();

            // get col name and value which can be inserted
            Reflection.ReflectionObject(target,
                element =>
                {
                    object[] attrs = element.GetCustomAttributes(false);
                    foreach (object attr in attrs)
                    {
                        NotInsertAble notInsert = attr as NotInsertAble;
                        NotDataColumn notDataColumn = attr as NotDataColumn;
                        if (notInsert != null || notDataColumn != null)
                        {
                            //dont use
                            return;
                        }
                    }

                    string propName = element.Name;
                    colNames.Add(propName);
                }
            );

            return colNames.ToArray();
        }
    }

}
