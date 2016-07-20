﻿using System;
using System.Net.Http;
using CRM.BLL;
using CRM.IBLL;

namespace CRM.WebApi.Controllers.Users
{
    /// <summary>
    /// 用户信息修改
    /// </summary>
    public class UpdateProfileController : BaseApiController
    {
        readonly IUserBaseService _userBaseService = new UserBaseService();
        /// <summary>
        /// 第三方用户登录
        /// </summary>
        public HttpResponseMessage Get(string token, string nickname, string avatar, int? gender)
        {
            return base.Wrapper(()=>
            {
                int userId = base.GetUserIdByToken(token);
                return this._userBaseService.Modify(userId,nickname, avatar, gender);
            });
        }
    }
}