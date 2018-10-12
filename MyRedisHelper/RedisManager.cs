using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedisHelper
{
    public class RedisManager
    {
        PooledRedisClientManager _prcm;

        public RedisManager(string redisPath)
        {
            _prcm = new PooledRedisClientManager(new string[] { redisPath }, new string[] { redisPath }, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 1, // “写”链接池链接数 
                MaxReadPoolSize = 1, // “读”链接池链接数 
                AutoStart = true,
            });

            ChangeDb(1);
        }

        public void ChangeDb(int db)
        {
            IRedisClient Redis = _prcm.GetClient();
            RedisClient redisClient = (RedisClient)Redis;
            try
            {
                redisClient.ChangeDb(db);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Set<T>(string key, T value)
        {
            IRedisClient Redis = _prcm.GetClient();
            RedisClient redisClient = (RedisClient)Redis;
            redisClient.ChangeDb(1);
            //放入内存
            Redis.Set<T>(key, value);
        }

        public T Get<T>(string key)
        {
            IRedisClient Redis = _prcm.GetClient();
            return Redis.Get<T>(key);
        }

        public string GetFromHash(string hashId, string key)
        {
            IRedisClient Redis = _prcm.GetClient();
            return Redis.GetValueFromHash(hashId, key);
        }

        public void Save()
        {
            IRedisClient Redis = _prcm.GetClient();
            //保存到硬盘
            Redis.SaveAsync();
            //释放内存
            Redis.Dispose();
        }

    }
}
