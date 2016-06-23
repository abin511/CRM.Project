using System.Collections.Generic;
using CRM.Model;

namespace CRM.Common
{
    public class EntityCompare:IEqualityComparer<ActionGroup>
    {
        /// <summary>
        /// 对其进行比较过滤
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(ActionGroup x, ActionGroup y)
        {
            return x.ID.Equals(y.ID);
        }

        /// <summary>
        /// 获取的哈希值，不同的ID值传递一个不同的值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(ActionGroup obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
