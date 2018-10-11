using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDbHelper.Tests
{
    [TestClass()]
    public class DbQueryTests
    {
        [TestMethod()]
        public void GetDataTest()
        {
            checkCount(0);

            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            string cmdStr = "select count(1) from dbo.test";
            List<int> ilist = DbQuery<int>.GetDataAsync(connectStr, cmdStr);
            Assert.AreEqual(ilist[0], 0);
        }

        [TestMethod()]
        public void GetDataParamsTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            string cmdStr = "select count(1) from dbo.test Where id > @id";
            List<int> ilist = DbQuery<int>.GetDataAsync(connectStr, cmdStr, new KeyValuePair<string, dynamic>[] { new KeyValuePair<string, dynamic>("id", 0) });
            Assert.AreEqual(ilist[0], 0);
        }

        [TestMethod()]
        public void ExcTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            string cmdStr = "INSERT INTO [dbo].[test] ([varchar] ,[int] ,[datetime]) VALUES (@varchar,@int,@datetime)";
            DbQuery.ExcAsync(connectStr, cmdStr, new KeyValuePair<string, dynamic>[]
            {
                new KeyValuePair<string, dynamic>("varchar", ""),
                new KeyValuePair<string, dynamic>("int", 0),
                new KeyValuePair<string, dynamic>("datetime", "2018-01-01")
            });

            checkCount(1);

            cmdStr = "delete dbo.test";
            DbQuery.ExcAsync(connectStr, cmdStr);

            checkCount(0);
        }

        private void checkCount(int c)
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            string cmdStr = "select count(1) from dbo.test";
            List<int> ilist = DbQuery<int>.GetData(connectStr, cmdStr);
            Assert.AreEqual(ilist[0], c);
        }
    }
}