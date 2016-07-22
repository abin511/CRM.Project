using System;
using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Users
{
    /// <summary>
    /// 用户退出
    /// </summary>
    public class LoginOutController : BaseApiController
    {
        private readonly ILoginOutService _loginOutService = new LoginOutService();
        /// <summary>
        /// 主播登出
        /// </summary>
        public HttpResponseMessage GetLoginOutByLive(string token,int roomId)
        {
            return base.Wrapper(()=>
            {
                int userId = base.GetUserIdByToken(token);
                return this._loginOutService.LoginOutByLive(userId, roomId);
            });
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        public HttpResponseMessage GetLoginOutByUser(string token, int roomId,int recordId)
        {
            return base.Wrapper(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return this._loginOutService.LoginOutByUser(userId, roomId, recordId);
            });
        }
    }
}
