using MyDbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Tests
{
    class ErrTestStructForDbTableManager : DbTable
    {
        public int id { get; set; }
        public int intC { get; set; }
        public string varchar10 { get; set; }
        public DateTime datetime { get; set; }
        public string varcharNull10 { get; set; }
        public int intNull { get; set; }
        public DateTime datetimeNull { get; set; }

        public string GetTableName()
        {
            return "test";
        }
    }

    class TestStructForDbTableManager : DbTable
    {
        [NotInsertAble]
        public int id { get; set; }
        public int intC { get; set; }
        public string varchar10 { get; set; }
        public DateTime datetime { get; set; }
        public string varcharNull10 { get; set; }
        public int intNull { get; set; }
        public DateTime datetimeNull { get; set; }
        
        public string GetTableName()
        {
            return "test";
        }
    }

    class NoInsertCharNullTestStructForDbTableManager : DbTable
    {
        [NotInsertAble]
        public int id { get; set; }
        public int intC { get; set; }
        public string varchar10 { get; set; }
        public DateTime datetime { get; set; }
        [NotDataColumn]
        public string varcharNull10 { get; set; }
        public int intNull { get; set; }
        public DateTime datetimeNull { get; set; }
        
        public string GetTableName()
        {
            return "test";
        }
    }
}
