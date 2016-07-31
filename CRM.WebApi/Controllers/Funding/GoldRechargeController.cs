using System.Net.Http;
using System.Web.Mvc;
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
        [HttpPost]
        public HttpResponseMessage Recharge(string token,decimal amount)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._userAccountService.Recharge(userId, amount);
            }, token);
        }
    }
}
