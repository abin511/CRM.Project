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
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 已经删除
        /// </summary>
        Deleted = 1
    }

    public enum LoginTypeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
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
    public enum MoneyChangeTypeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 充值
        /// </summary>
        Recharge = 1,
        /// <summary>
        /// 提现
        /// </summary>
        Withdrawal = 2
    }
    public enum GoldChangeTypeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 增加金币
        /// </summary>
        Increase = 1,
        /// <summary>
        /// 减少金币
        /// </summary>
        Reduce = 2
    }
}
