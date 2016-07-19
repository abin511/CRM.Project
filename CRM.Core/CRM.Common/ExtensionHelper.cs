using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using CRM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRM.Common
{
   public static partial class ExtensionHelper
    {
        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this DateTime target, long timestamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return start.AddSeconds(timestamp);
        }

       public static int ToInt(this string str)
       {
           int number = 0;
           if (!int.TryParse(str, out number))
           {
               number =0;
           }
           return number;
       }
        /// <summary>
        /// 转化为json字符串。
        /// </summary>
        public static string ToJson<T>(this T data)
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
        private static T ToObject<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return default(T);
            try
            {
                if ((typeof(T)).Name.ToLower() == "string")
                {
                    return (T)Convert.ChangeType(jsonString, typeof(T));
                }
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public static JContainer GetContainer(this string json)
        {
            var jContainer = JsonConvert.DeserializeObject(json) as JContainer;
            return jContainer;
        }
        /// <summary>
        /// Json数据转数组
        /// </summary>
        /// <returns></returns>
        public static List<T> JsonToList<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return null;
            }
            List<T> list = new List<T>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(list.GetType());
            byte[] bytelist = System.Text.Encoding.UTF8.GetBytes(jsonString);
            using (System.IO.MemoryStream menorystream = new System.IO.MemoryStream(bytelist))
            {
                menorystream.Position = 0;
                list = (List<T>)json.ReadObject(menorystream);
                menorystream.Close();
            }
            return list;
        }
        public static T GetKey<T>(this CustomConfig config)
        {
            if (config == null ||  string.IsNullOrEmpty(config.Value))
            {
                return default(T);
            }
            return config.Value.ToObject<T>();
        }
    }
}
