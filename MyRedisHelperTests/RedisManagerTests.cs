using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyRedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRedisHelper.Tests
{
    [TestClass()]
    public class RedisManagerTests
    {
        [TestMethod()]
        public void Test()
        {
            string connectStr = "127.0.0.1:6379";
            try
            {
                const int value = 123;
                const string key = "test1";
                RedisManager redisManager = new RedisManager(connectStr);
                redisManager.Set<int>(key, value);
                int result = redisManager.Get<int>(key);
                Assert.AreEqual(value, result);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void SetTest()
        {
            string connectStr = "127.0.0.1:6379";
            try
            {
                const int value = 1489674315;
                const string key = "test2";
                RedisManager redisManager = new RedisManager(connectStr);
                redisManager.Set<int>(key, value, 61);

                bool isExist = redisManager.IsKeyExist(key);
                int result = redisManager.Get<int>(key);

                Assert.AreEqual(true, isExist);
                Assert.AreEqual(value, result);


                const string key1 = "test2t";
                redisManager.Set<int>(key1, value, 1);
                Thread.Sleep(1500);

                isExist = redisManager.IsKeyExist(key1);
                result = redisManager.Get<int>(key1);

                Assert.AreEqual(false, isExist);
                Assert.AreNotEqual(value, result);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void RemoveHashTest()
        {
            string connectStr = "127.0.0.1:6379";
            try
            {
                const string value = "1489674315";
                const string key = "test3";
                const string hashId = "hash1";
                RedisManager redisManager = new RedisManager(connectStr);
                redisManager.SetInHash(hashId, key, value);

                bool isExist = redisManager.IsKeyExist(hashId);
                string result = redisManager.GetFromHash(hashId, key); ;
                redisManager.RemoveFromHash(hashId, key);
                string noResult = redisManager.GetFromHash(hashId, key); ;

                Assert.AreEqual(true, isExist);
                Assert.AreEqual(value, result);
                Assert.AreNotEqual(value, noResult);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void SubscriptionTest()
        {
            string connectStr = "127.0.0.1:6379";
            bool isDone = false;
            bool subscriptionTest = true;
            try
            {
                const string channel = "test";
                Thread thread = new Thread(new ParameterizedThreadStart(
                    (cha) =>
                    {
                        try
                        {
                            RedisManager subscriptionRedisManager = new RedisManager(connectStr);
                            subscriptionRedisManager.Subscription(cha.ToString(),
                                (cnl, msg) =>
                                {
                                    Assert.AreEqual("TESTMSG", msg);
                                    subscriptionRedisManager.UnSubscription(cnl);
                                }
                            );
                        }
                        catch (Exception e)
                        {
                            subscriptionTest = false;
                        }
                        finally
                        {
                            isDone = true;
                        }
                    }
                    ));
                thread.Start(channel);

                RedisManager PublishRedisManager = new RedisManager(connectStr);
                
                

                while (!isDone)
                {
                    Thread.Sleep(1000);
                    PublishRedisManager.Publish(channel, "TESTMSG", 1);
                }

                if (!subscriptionTest)
                    throw new Exception("fail");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}