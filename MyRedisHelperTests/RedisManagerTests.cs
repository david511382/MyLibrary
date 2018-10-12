using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyRedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedisHelper.Tests
{
    [TestClass()]
    public class RedisManagerTests
    {
        [TestMethod()]
        public void ChangeDbTest()
        {
            string connectStr = "127.0.0.1:6379";
            try
            {
                const int value = 123;
                const string key = "test";
                RedisManager redisManager = new RedisManager(connectStr);
                redisManager.Set<int>(key, value);
                int result = redisManager.Get<int>(key);
                Assert.AreEqual(value, result);
            }
            catch(Exception e)
            {
                Assert.Fail();
            }
        }
    }
}