using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace CRM.Common
{
    public class HttpRuntimeCache: ICacheHelper
    {
        /// <summary>
        ///  设置缓存绝对过期时间
        /// </summary>
        public void Add(string key, object value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }
        /// <summary>
        /// 设置缓存绝对过期时间
        /// </summary>
        public void Add(string key, object value, TimeSpan expireDate)
        {
            //设置滑动过期时间，只要刷新缓存，就一直存在
            //HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, expireDate, CacheItemPriority.High, null);
            var ex = DateTime.Now.AddTicks(expireDate.Ticks);
            HttpRuntime.Cache.Insert(key, value, null, ex, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        public Object Get(string key)
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
            Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}
