using System;
using Memcached.Client;

namespace CRM.Common
{
    public class MemcachedCache : ICacheHelper
    {
        //单例模式
        public static readonly MemcachedClient mc;
        static MemcachedCache()
        {
            string[] servers = { "127.0.0.1:11211" };

            //初始化池 
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            mc = new MemcachedClient {EnableCompression = false};
        }
        public void Add(string key, object value, DateTime expireDate)
        {
            if (mc.KeyExists(key))
            {
                mc.Set(key, value, expireDate);
            }
            else
            {
                mc.Add(key, value, expireDate);
            }
        }
        public void Add(string key, object value, TimeSpan expireDate)
        {
            var ex = DateTime.Now.AddTicks(expireDate.Ticks);
            Add(key, value,ex);
        }

        public void Add<T>(string key, T value, DateTime expireDate)
        {
            Add(key, value.ToString(), expireDate);
        }
        public void Add<T>(string key, T value, TimeSpan expireDate)
        {
            var ex = DateTime.Now.AddTicks(expireDate.Ticks);
            Add(key, value.ToString(), ex);
        }
        public T Get<T>(string key) where T : class
        {
            return mc.Get(key) as T;
        }
        public Object Get(string key)
        {
            return mc.Get(key);
        }
        public bool Remove(string key)
        {
            if(mc.KeyExists(key))
                return mc.Delete(key);
            return false;
        }
    }
}
