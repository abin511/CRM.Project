﻿namespace CRM.Model
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
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 已经删除
        /// </summary>
        Deleted = 1
    }
}
