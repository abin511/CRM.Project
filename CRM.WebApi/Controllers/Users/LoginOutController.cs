using System;
using System.Net.Http;
using System.Web.Mvc;
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
        [HttpPost]
        public HttpResponseMessage LoginOutByLive(string token,int roomId)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._loginOutService.LoginOutByLive(userId, roomId);
            }, token);
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        [HttpPost]
        public HttpResponseMessage LoginOutByUser(string token, int roomId,int recordId)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._loginOutService.LoginOutByUser(userId, roomId, recordId);
            }, token);
        }
    }
}
