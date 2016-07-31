using System;
using System.Net.Http;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Users
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class LoginController : BaseApiController
    {
        readonly IUserLoginOutSideService _userLoginOutSideService = new UserLoginOutSideService();
        readonly IUserLoginInSideService _userLoginInSideService = new UserLoginInSideService();
        /// <summary>
        /// 第三方用户登录
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Login(string openId, LoginTypeEnum loginType = LoginTypeEnum.None)
        {
            return base.WrapperTransaction((userId)=>
            {
                var result = this._userLoginOutSideService.Login(loginType, openId);
                var data = new Result<ViewUserData>
                {
                    Code = result.Code,
                    Msg = result.Msg,
                    Data = result.Code == ResultEnum.Error? null: new ViewUserData()
                        {
                            token = base.GetToken(result.Data),
                            liveurl = "http://www.xxx.com/room/"+ result.Data,
                            playurl = "http://www.xxx.com/"+ result.Data
                    }
                };
                return data;
            });
        }
        /// <summary>
        /// 本站注册用户登录
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Login(string uName, string uPwd, LoginTypeEnum loginType = LoginTypeEnum.None)
        {
            return base.WrapperTransaction((userId) =>
            {
                var result = this._userLoginInSideService.Login(new UserLoginInSide
                {
                    LoginType = (byte)loginType,
                    LoginName = uName,
                    LoginPwd = uPwd
                });
                var data = new Result<ViewUserData>
                {
                    Code = result.Code,
                    Msg = result.Msg,
                    Data = result.Code == ResultEnum.Error ? null : new ViewUserData()
                    {
                        token = base.GetToken(result.Data),
                        liveurl = "http://www.xxx.com/room/" + result.Data,
                        playurl = "http://www.xxx.com/" + result.Data
                    }
                };
                return data;
            });
        }
        /// <summary>
        /// 本站注册
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Register(string uName, string uPwd,string rePwd, LoginTypeEnum loginType = LoginTypeEnum.None)
        {
            return base.WrapperTransaction((userId) => this._userLoginInSideService.Register(new UserLoginInSide
            {
                LoginType = (byte)loginType,
                LoginName = uName,
                LoginPwd = uPwd
            }, rePwd));
        }
    }
}
