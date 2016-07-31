using System;
namespace CRM.Common
{
    public partial interface ICacheHelper
    {
        void Add(string key, object value, DateTime expireDate);
        void Add(string key, object value, TimeSpan expireDate);
        void Add<T>(string key, T value, DateTime expireDate);
        void Add<T>(string key, T value, TimeSpan expireDate);
        T Get<T>(string key) where T : class;
        Object Get(string key);
        bool Remove(string key);
    }
}
