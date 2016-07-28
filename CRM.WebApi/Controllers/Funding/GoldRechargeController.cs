using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Funding
{
    /// <summary>
    ///金币充值
    /// </summary>
    public class GoldRechargeController : BaseApiController
    {
        readonly IUserAccountService _userAccountService = new UserAccountService();
        /// <summary>
        /// 用户充值金币
        /// </summary>
        public HttpResponseMessage Get(string token,decimal amount)
        {
            return base.WrapperTransaction(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return this._userAccountService.Recharge(userId, amount);
            });
        }
    }
}
