using System;
namespace CRM.Common
{
    public partial interface ICacheHelper
    {
        /// <summary>
        ///  设置缓存绝对过期时间
        /// </summary>
        void Add(string key, object value, DateTime expireDate);
        /// <summary>
        /// 设置缓存绝对过期时间
        /// </summary>
        void Add(string key, object value, TimeSpan expireDate);
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        T Get<T>(string key) where T : class;
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        Object Get(string key);
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        bool Remove(string key);
    }
}
