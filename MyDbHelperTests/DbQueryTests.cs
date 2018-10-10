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
            List<int> ilist = DbQuery<int>.GetData("select count(1) from mvc.Members");
            Assert.AreEqual(ilist[0], 0);
        }
    }
}