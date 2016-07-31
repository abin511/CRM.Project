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
        Reduce = 2
    }
    public enum IntegralChangeTypeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 用户赠送礼物 增加积分
        /// </summary>
        IncreaseByGetGift = 1,
        /// <summary>
        /// 减少积分
        /// </summary>
        Reduce = 2
    }
    public enum UserBaseStausEnum
    {
        /// <summary>
        /// Normal 
        /// </summary>
        Normal = 0,
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
        ///  -1被锁定
        /// </summary>
        Lock = -1,
        /// <summary>
        /// 空闲
        /// </summary>
        Lazy = 0,
        /// <summary>
        /// 正在直播
        /// </summary>
        Live = 1
    }
}
