using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Tests
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
            List<int> ilist = DbQuery<int>.GetData(connectStr, cmdStr);
            Assert.AreEqual(ilist[0], 0);
        }

        [TestMethod()]
        public void GetDataParamsTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            string cmdStr = "select count(1) from dbo.test Where id > @id";
            List<int> ilist = DbQuery<int>.GetData(connectStr, cmdStr, new KeyValuePair<string, dynamic>[] { new KeyValuePair<string, dynamic>("id", 0) });
            Assert.AreEqual(ilist[0], 0);
        }

        [TestMethod()]
        public void ExcTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            string cmdStr = "INSERT INTO [dbo].[test] ([varchar10] ,[intC] ,[datetime]) VALUES (@varchar10,@intC,@datetime)";
            DbQuery.Exc(connectStr, cmdStr, new KeyValuePair<string, dynamic>[]
            {
                new KeyValuePair<string, dynamic>("varchar10", ""),
                new KeyValuePair<string, dynamic>("intC", 0),
                new KeyValuePair<string, dynamic>("datetime", DateTime.Now)
            });

            checkCount(1);

            cmdStr = "delete dbo.test";
            DbQuery.Exc(connectStr, cmdStr);

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