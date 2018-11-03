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
        public void InsertDataTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            int count;
            const string identity = "1234567890";
            ErrTestStructForDbTableManager errTestStruct = new ErrTestStructForDbTableManager();
            errTestStruct.id = 1;
            errTestStruct.intC = 1;
            errTestStruct.varchar10 = identity;
            errTestStruct.varcharNull10 = "";
            errTestStruct.datetime = DateTime.Now;
            errTestStruct.datetimeNull = DateTime.Now;

            try
            {
                DbTableManager<ErrTestStructForDbTableManager>.Insert(errTestStruct, connectStr);
                Assert.Fail();
            }
            catch
            {
                count = DbQuery<int>.GetData(connectStr, "select count(1) from " + errTestStruct.GetTableName() + " where varchar10 = '" + identity + "'")[0];
                Assert.AreEqual(count, 0);
            }
            finally
            {
                DbQuery.Exc(connectStr, "delete test");
            }

            TestStructForDbTableManager testStruct = new TestStructForDbTableManager();
            testStruct.id = 1;
            testStruct.intC = 1;
            testStruct.varchar10 = identity;
            testStruct.varcharNull10 = "";
            testStruct.datetime = DateTime.Now;
            testStruct.datetimeNull = DateTime.Now;

            try
            {
                DbTableManager<TestStructForDbTableManager>.Insert(testStruct, connectStr);

                count = DbQuery<int>.GetData(connectStr, "select count(1) from " + errTestStruct.GetTableName() + " where varchar10 = '" + identity + "'")[0];
                Assert.AreEqual(count, 1);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                DbQuery.Exc(connectStr, "delete test");
            }

            NoInsertCharNullTestStructForDbTableManager noCharNullTestStruct = new NoInsertCharNullTestStructForDbTableManager();
            const string testvalue = "1234";
            noCharNullTestStruct.id = 1;
            noCharNullTestStruct.intC = 1;
            noCharNullTestStruct.varchar10 = identity;
            noCharNullTestStruct.varcharNull10 = testvalue;
            noCharNullTestStruct.datetime = DateTime.Now;
            noCharNullTestStruct.datetimeNull = DateTime.Now;

            try
            {
                DbTableManager<NoInsertCharNullTestStructForDbTableManager>.Insert(noCharNullTestStruct, connectStr);

                string value = DbQuery<string>.GetData(connectStr, "select varcharNull10 from " + noCharNullTestStruct.GetTableName() + " where varchar10 = '" + identity + "'")[0];
                Assert.AreEqual(value, null);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                DbQuery.Exc(connectStr, "delete test");
            }
        }

        [TestMethod()]
        public void InsertDatasTest()
        {
            string connectStr = @"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=UnitTest;Integrated Security=True";
            int count;
            const string identity = "1234567890";
            const int times = 123;
            List<NoInsertCharNullTestStructForDbTableManager> datas = new List<NoInsertCharNullTestStructForDbTableManager>();
            for (int i = 0; i < times; i++)
            {
                NoInsertCharNullTestStructForDbTableManager noCharNullTestStruct = new NoInsertCharNullTestStructForDbTableManager();
                const string testvalue = "1234";
                noCharNullTestStruct.id = 1;
                noCharNullTestStruct.intC = 1;
                noCharNullTestStruct.varchar10 = identity;
                noCharNullTestStruct.varcharNull10 = testvalue;
                noCharNullTestStruct.datetime = DateTime.Now;
                noCharNullTestStruct.datetimeNull = DateTime.Now;
                datas.Add(noCharNullTestStruct);
            }

            try
            {
                DbTableManager<NoInsertCharNullTestStructForDbTableManager>.Insert(datas, connectStr);

                count  = DbQuery<int>.GetData(connectStr, "select Count(1) from " + datas[0].GetTableName() + " where varchar10 = '" + identity + "'")[0];
                Assert.AreEqual(count, times);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            finally
            {
                DbQuery.Exc(connectStr, "delete test");
            }

        }
    }
}