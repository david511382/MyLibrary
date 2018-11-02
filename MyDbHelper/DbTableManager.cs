using MyReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDbHelper
{
    public class NotInserAble : Attribute
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
            List<KeyValuePair<string, object>> colNameAndValues= new List<KeyValuePair<string, object>>();
            
            // get col name and value which can be inserted
            Reflection.ReflectionObject(tableStruct,
                element =>
                {
                    object obj;
                    obj = element.GetValue(tableStruct);
                    string propertyValue = obj?.ToString();
                    string propName = element.Name;
                    
                    bool isUse = true;
                    object[] attrs = element.GetCustomAttributes(false);
                    foreach (object attr in attrs)
                    {
                        NotInserAble authAttr = attr as NotInserAble;
                        if (authAttr != null)
                        {
                            //dont use
                            isUse = false;
                        }
                    }

                    if (isUse)
                        colNameAndValues.Add(new KeyValuePair<string, object>(propName, propertyValue));
                }
            );

            if (colNameAndValues.Count == 0)
                return;

            string insertSql = "INSERT INTO "+ tableStruct.GetTableName() + " (";
            string insertSqlValue = "VALUES (";
            string s = ",";
            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>();
            string colName;
            for (int i = 0; i < colNameAndValues.Count; i++)
            {
                if (i == colNameAndValues.Count - 1)
                    s = "";
                colName = colNameAndValues[i].Key;
                insertSql += " " + colName + s;
                insertSqlValue += " @" + colName + s;
                parameters.Add(new KeyValuePair<string, dynamic>(colName, colNameAndValues[i].Value));
            }
            insertSql += " ) " + insertSqlValue + " ) ";
            DbQuery.Exc(connectStr, insertSql, parameters.ToArray());
        }
    }

}
