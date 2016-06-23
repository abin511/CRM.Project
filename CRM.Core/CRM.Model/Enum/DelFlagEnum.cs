using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
    public enum DelFlagEnum
    {
        /// <summary>
        /// 未删除
        /// </summary>
        Normal=0,

        /// <summary>
        /// 已经删除
        /// </summary>
        Deleted=1
    }
}
