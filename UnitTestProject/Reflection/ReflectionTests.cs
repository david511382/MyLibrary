using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyReflection;
using MyReflectionTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection.Tests
{
    [TestClass()]
    public class ReflectionTests
    {
        [TestMethod()]
        public void ReflectionObjectTest()
        {
            TestClass testClass = new TestClass();

            List<KeyValuePair<string, object>> result = Reflection.ReflectionObject(testClass);

            Assert.AreEqual(result[0].Key, "i");
            Assert.AreEqual(Int32.Parse(result[0].Value.ToString()), 11);

            Assert.AreEqual(result[1].Key, "s");
            Assert.AreEqual((result[1].Value).ToString(), "fd");

            Assert.AreEqual(result[2].Key, "b");
            Assert.AreEqual(bool.Parse(result[2].Value.ToString()), false);

            Assert.AreEqual(result[3].Key, "f");
            Assert.AreEqual(float.Parse(result[3].Value.ToString()), 1.12f);

            Assert.AreEqual(result[4].Key, "d");
            Assert.AreEqual(double.Parse(result[4].Value.ToString()), 23.4324343d);

            Assert.AreEqual(result[5].Key, "dt");
            Assert.AreEqual(DateTime.Parse(result[5].Value.ToString()), new DateTime(1999, 11, 23, 3, 32, 44));

            Assert.AreEqual(result[6].Key, "l");
            Assert.AreEqual(long.Parse(result[6].Value.ToString()), 2343435353535);

            Assert.AreEqual(result.Count, 7);
        }
    }
}