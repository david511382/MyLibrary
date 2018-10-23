using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDbHelper
{
    class BulkCopy
    {
        private string _targetConnStr, _tableName;

        public BulkCopy(string toDb, string tableName)
        {
            ChangeConnStr(toDb, tableName);
        }

        public void WriteToTarget(DataTable dataTable, int batchSize, bool breakWhenColConflict = false)
        {
            //set data col name
            DataTable data = GetTargetColFormat();

            //组装数据
            foreach (var item in dataTable.Rows)
            {
                DataRow dataRow = data.NewRow();
                for (int i = 0; i < data.Columns.Count; ++i)
                {
                    string columnName = ((System.Data.DataColumn)(data.Columns[i])).Caption;

                    #region check col conflict
                    if (breakWhenColConflict)
                        if (!columnName.Equals(((System.Data.DataColumn)(dataTable.Columns[i])).Caption))
                            throw new Exception("column conflict");
                    #endregion

                    dataRow[columnName] = ((DataRow)item)[columnName];
                }

                lock (data)
                {
                    data.Rows.Add(dataRow);
                }
            }

            SqlBulkCopy sqlbul = new SqlBulkCopy(_targetConnStr);
            sqlbul.BatchSize = batchSize;
            sqlbul.DestinationTableName = _tableName;
            sqlbul.BulkCopyTimeout = 0;
            sqlbul.WriteToServer(data);

            data.Dispose();
        }

        public void ChangeConnStr(string targetDb, string table = null)
        {
            _targetConnStr = targetDb;
            _tableName = (string.IsNullOrEmpty(table)) ? _tableName : table;
        }

        private DataTable GetTargetColFormat()
        {
            string strColumns = "SELECT  name ,TYPE_NAME(system_type_id) AS column_type FROM sys.columns WHERE object_id=OBJECT_ID(N'" +
                                _tableName + "')";
            DataTable targetTable = DbQuery.GetData(strColumns, _targetConnStr);

            DataTable data = new DataTable();
            string colName = string.Empty;
            for (int i = 0; i < targetTable.Rows.Count; i++)
            {
                colName = targetTable.Rows[i].ItemArray[0].ToString();
                //组装插入表的行
                string columnType = targetTable.Rows[i].ItemArray[1].ToString();
                data.Columns.Add(colName, GetColumnsType(columnType));
            }

            return data;
        }

        private Type GetColumnsType(string columnType)
        {
            //组装插入表的行
            if (columnType.Contains("int"))
            {
                return typeof(long);
            }
            else if (columnType.Contains("char") || columnType.Contains("text"))
            {
                return typeof(string);
            }
            else if (columnType.Contains("decimal"))
            {
                return typeof(decimal);
            }
            else if (columnType.Contains("bit"))
            {
                return typeof(byte);
            }
            else if (columnType.Contains("datetime"))
            {
                return typeof(DateTime);
            }

            throw new Exception("undefind column type");
        }
    }
}
