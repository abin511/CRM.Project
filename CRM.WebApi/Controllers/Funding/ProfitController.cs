using System.Net.Http;
using System.Web.Mvc;
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
        [HttpPost]
        public HttpResponseMessage Profit(string token,int gold)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._userAccountService.Profit(userId, gold);
            }, token);
        }
    }
}
