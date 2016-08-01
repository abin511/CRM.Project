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
        [AcceptVerbs("Fans")]
        public HttpResponseMessage GetFans(string token = "")
        {
            return base.WrapperTransaction((userId) =>
            {
                return new Result<List<UserFans>>()
                {
                    Code = ResultEnum.Success,
                    Data = this._userFansService.Get(m => m.UserId == userId).ToList()
                };
            },token);
        }
        /// <summary>
        /// 获取关注信息
        /// </summary>
        [AcceptVerbs("Sub")]
        public HttpResponseMessage GetSubScription(string token)
        {
            return base.WrapperTransaction((userId) =>
            {
                return new Result<List<UserFans>>()
                {
                    Code = ResultEnum.Success,
                    Data = this._userFansService.Get(m => m.FansId == userId).ToList()
                };
            },token);
        }
    }
}
