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
    public class DbTableManagerTests
    {
        [TestMethod()]
        public void GetDataTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            int count;
            ErrTestStructForDbTableManager errTestStruct = new ErrTestStructForDbTableManager();
            errTestStruct.lotterycode = "test";
            errTestStruct.add_time = DateTime.Now;
            errTestStruct.open_time = DateTime.Now;

            try
            {
                DbTableManager<ErrTestStructForDbTableManager>.Insert(errTestStruct, connectStr);
                Assert.Fail();
            }
            catch
            {
                count = DbQuery<int>.GetData(connectStr, "select count(1) from " + errTestStruct.GetTableName() + " where lotterycode = 'test'")[0];
                Assert.AreEqual(count, 0);
            }

            TestStructForDbTableManager testStruct = new TestStructForDbTableManager();
            testStruct.lotterycode = "test";
            testStruct.add_time = DateTime.Now;
            testStruct.open_time = DateTime.Now;
            try
            {
                DbTableManager<TestStructForDbTableManager>.Insert(testStruct, connectStr);

                count = DbQuery<int>.GetData(connectStr, "select count(1) from " + errTestStruct.GetTableName() + " where lotterycode = 'test'")[0];
                Assert.AreEqual(count, 1);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}