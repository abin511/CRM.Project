using System;

namespace CRM.Model
{
    public enum ResultEnum
    {
        /// <summary>
        /// 操作失败
        /// </summary>
        Error = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1
    }
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
    public enum LoginTypeEnum
    {
        /// <summary>
        /// 未删除
        /// </summary>
        Normal = 0,
        /// <summary>
        /// QQ
        /// </summary>
        Q = 1,
        /// <summary>
        /// 微信
        /// </summary>
        W = 2,
        /// <summary>
        /// 手机注册
        /// </summary>
        M = 3,
    }
}
