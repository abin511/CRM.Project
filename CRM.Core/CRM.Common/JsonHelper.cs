using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRM.Common
{
    /// <summary>
    /// Json转换操作类。
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 转化为json字符串。
        /// </summary>
        public static string ToJson<T>(T data)
        {
            try
            {
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// json字符串转化成特定的Object.
        /// </summary>
        public static T ToObject<T>(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static JContainer GetContainer(string json)
        {
            var jContainer = JsonConvert.DeserializeObject(json) as JContainer;
            return jContainer;
        }
    }
}
