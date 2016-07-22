using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Users
{
    /// <summary>
    /// 获取粉丝信息
    /// </summary>
    public class UserFansController : BaseApiController
    {
        readonly IUserFansService _userFansService = new UserFansService();
        /// <summary>
        /// 获取粉丝信息
        /// </summary>
        [HttpGet]
        [Route("api/userfans/fans")]
        public HttpResponseMessage GetFans(string token = "36B2FF88CC41BE9A552524424ACC148AE2468AEF4812A21D")
        {
            return base.Wrapper(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return new Result<List<UserFans>>()
                {
                    Code = ResultEnum.Success,
                    Data = this._userFansService.Get(m => m.UserId == userId).ToList()
                };
            });
        }
        /// <summary>
        /// 获取关注信息
        /// </summary>
        [HttpGet]
        [Route("api/userfans/sub")]
        public HttpResponseMessage GetSubScription(string token)
        {
            return base.Wrapper(() =>
            {
                int userId = base.GetUserIdByToken(token);
                return new Result<List<UserFans>>()
                {
                    Code = ResultEnum.Success,
                    Data = this._userFansService.Get(m => m.FansId == userId).ToList()
                };
            });
        }
    }
}
