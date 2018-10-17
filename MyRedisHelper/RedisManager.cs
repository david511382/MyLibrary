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
        private PooledRedisClientManager _prcm;
        private IRedisSubscription _redisSubscription;

        public RedisManager(string redisPath)
        {
            _prcm = new PooledRedisClientManager(new string[] { redisPath });
        }

        public RedisManager(string readPath, string writePath)
        {
            _prcm = new PooledRedisClientManager(
                new string[] { readPath },
                new string[] { writePath },
                new RedisClientManagerConfig
                {
                    MaxWritePoolSize = 1, // “写”链接池链接数 
                    MaxReadPoolSize = 1, // “读”链接池链接数 
                    AutoStart = true
                });
        }

        public void Subscription(string channelName, Action<string, string> onMessage, Action<string> OnSubscribe = null, Action<string> OnUnSubscribe = null)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                if (_redisSubscription == null)
                    _redisSubscription = Redis.CreateSubscription();
                //接收消息处理Action
                _redisSubscription.OnMessage = (changel, msg) =>
                {
                    onMessage?.Invoke(changel, msg);
                };

                if (OnSubscribe != null)
                {
                    //订阅事件处理
                    _redisSubscription.OnSubscribe = (channel) =>
                    {
                        OnSubscribe?.Invoke(channel);
                    };
                }

                if (OnUnSubscribe != null)
                {
                    //取消订阅事件处理
                    _redisSubscription.OnUnSubscribe = (channel) =>
                    {
                        OnUnSubscribe?.Invoke(channel);
                    };
                }

                //订阅频道
                _redisSubscription.SubscribeToChannels(channelName);
            }
        }

        public void UnSubscription(string channelName)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                _redisSubscription?.UnSubscribeFromAllChannels();
                _redisSubscription = null;
            }
        }

        public void Publish(string channelName, string msg, int index)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                Redis.PublishMessage(channelName, string.Format(msg, index));
            }
        }

        public void ChangeDb(int db)
        {
            try
            {
                using (IRedisClient Redis = _prcm.GetClient())
                {
                    RedisClient redisClient = (RedisClient)Redis;
                    redisClient.ChangeDb(db);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Set<T>(string key, T value)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                Redis.Set<T>(key, value);
            }
        }

        public void Set<T>(string key, T value, int expire)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                Redis.Set<T>(key, value, new TimeSpan(0, 0, expire));
            }
        }

        public void SetInHash(string hashId, string key, string value)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                Redis.SetEntryInHash(hashId, key, value);
            }
        }

        public bool IsKeyExist(string key)
        {
            bool isExist = false;
            using (IRedisClient Redis = _prcm.GetClient())
            {
                isExist = Redis.ContainsKey(key);
            }
            return isExist;
        }

        public T Get<T>(string key)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                return Redis.Get<T>(key);
            }
        }

        public string GetFromHash(string hashId, string key)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                return Redis.GetValueFromHash(hashId, key);
            }
        }

        public List<string> GetKeysFromHash(string hashId)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                return Redis.GetHashKeys(hashId);
            }
        }

        public bool Remove(string key)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                return Redis.Remove(key);
            }
        }

        public bool RemoveFromHash(string hashId, string key)
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                return Redis.RemoveEntryFromHash(hashId, key);
            }
        }

        public void Save()
        {
            using (IRedisClient Redis = _prcm.GetClient())
            {
                //保存到硬盘
                Redis.SaveAsync();
                //释放内存
                Redis.Dispose();
            }
        }

    }
}
