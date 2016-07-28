using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Trading
{
    /// <summary>
    ///金币消费
    /// </summary>
    public class GoldCostController : BaseApiController
    {
        readonly IUserAccountService _userAccountService = new UserAccountService();
        /// <summary>
        /// 金币消费 赠送主播礼物
        /// </summary>
        public HttpResponseMessage Get(string token,int roomId,int giftId)
        {
            return base.WrapperTransaction(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return this._userAccountService.Cost(userId, roomId, giftId);
            });
        }
    }
}
