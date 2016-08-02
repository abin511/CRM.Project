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
        /// 关注某人
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Attention(string token,string userNumber)
        {
            return base.WrapperTransaction((userId) => this._userFansService.Attention(userId, userNumber), token);
        }
        /// <summary>
        /// 关注某人
        /// </summary>
        [HttpGet]
        public HttpResponseMessage IsAttention(string token, string userNumber)
        {
            return base.WrapperTransaction((userId) => this._userFansService.IsAttention(userId, userNumber), token);
        }
        /// <summary>
        /// 获取粉丝信息
        /// </summary>
        [AcceptVerbs("Fans")]
        public HttpResponseMessage GetFans(string token)
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
        [AcceptVerbs("Atten")]
        public HttpResponseMessage GetAttention(string token)
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
