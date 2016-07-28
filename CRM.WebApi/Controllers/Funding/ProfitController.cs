using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Funding
{
    /// <summary>
    ///收益兑换
    /// </summary>
    public class ProfitController : BaseApiController
    {
        readonly IUserAccountService _userAccountService = new UserAccountService();
        /// <summary>
        /// 收益兑换
        /// </summary>
        public HttpResponseMessage Get(string token,int gold)
        {
            return base.WrapperTransaction(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return this._userAccountService.Profit(userId, gold);
            });
        }
    }
}
