using System.Net.Http;
using System.Web.Mvc;
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
        [HttpPost]
        public HttpResponseMessage Cost(string token,int roomId,int giftId)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._userAccountService.Cost(userId, roomId, giftId);
            }, token);
        }
    }
}
