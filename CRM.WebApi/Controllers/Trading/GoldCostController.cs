using System.Linq;
using System.Net.Http;
using CRM.BLL;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

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
            return base.Wrapper<int>(() =>
            {
                int userId = base.GetUserIdByToken(token);
                var result = _userAccountService.Cost(userId, roomId, giftId);
                return new Result<int>()
                {
                    Code = ResultEnum.Success,
                    Data = result.Data
                };
            });
        }
    }
}
