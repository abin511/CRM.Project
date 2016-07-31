using System;
using System.Collections;
using System.Web;

namespace CRM.Common
{
    public class HttpRuntimeCache: ICacheHelper
    {
        public void Add(string cacheKey, object value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, TimeSpan.Zero);
        }
        public void Add(string key, object value, TimeSpan expireDate)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, expireDate, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }
        public void Add<T>(string cacheKey, T value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, TimeSpan.Zero);
        }
        public void Add<T>(string key, T value, TimeSpan expireDate)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, expireDate, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        public object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        public T Get<T>(string key) where T : class
        {
            object cache = HttpRuntime.Cache[key];
            if (cache == null) return null;
            try
            {
                return cache as T;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public bool Remove(string key)
        {
            object cache = HttpRuntime.Cache[key];
            if (cache == null) return false;
            HttpRuntime.Cache.Remove(key);
            return true;
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAll()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                _cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}
