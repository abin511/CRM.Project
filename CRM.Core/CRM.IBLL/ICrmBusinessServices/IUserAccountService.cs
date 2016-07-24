using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserAccountService
    {
        /// <summary>
        /// 用户充值 增加金币
        /// </summary>
        /// <returns></returns>
        Result<long> Recharge(int userId, decimal amount);
        /// <summary>
        /// 用户购买礼物 赠送主播
        /// </summary>
        /// <returns></returns>
        Result<long> Cost(int userId, int roomId, int giftId);
        /// <summary>
        /// 用户金币兑换
        /// </summary>
        /// <returns></returns>
        Result<decimal> Profit(int userId, int gold);
    }
}
