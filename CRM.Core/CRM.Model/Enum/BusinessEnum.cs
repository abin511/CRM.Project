namespace CRM.Model
{
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
        Reduce = -1
    }
    public enum UserBaseStausEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 被锁定
        /// </summary>
        Lock = -1,
        /// <summary>
        /// 被封号
        /// </summary>
        Sealed = -2
    }
    public enum RoomStausEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 被锁定
        /// </summary>
        Lock = -1
    }
}
