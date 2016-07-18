using System;
using System.Net.Http;
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
        public HttpResponseMessage GetLogin(LoginTypeEnum loginType, string openId)
        {
            return base.Wrapper<ViewUserData>(()=>
            {
                var result = _userLoginOutSideService.Login(loginType, openId);
                var data = new Result<ViewUserData>
                {
                    Code = result.Code,
                    Msg = result.Msg,
                    Data = result.Code == ResultEnum.Error? null: new ViewUserData()
                        {
                            token = base.GetTokenByUserId(result.Data),
                            liveurl = "",
                            playurl = ""
                        }
                };
                return data;
            });
        }
        /// <summary>
        /// 本站注册用户登录
        /// </summary>
        public HttpResponseMessage GetLogin(LoginTypeEnum loginType, string uName, string uPwd)
        {
            return base.Wrapper<ViewUserData>(() =>
            {
                var model = new UserLoginInSide
                {
                    LoginType = (int)loginType,
                    LoginName = uName,
                    LoginPwd = uPwd
                };
                var result = _userLoginInSideService.Login(model);
                var data = new Result<ViewUserData>
                {
                    Code = result.Code,
                    Msg = result.Msg,
                    Data = result.Code == ResultEnum.Error ? null : new ViewUserData()
                    {
                        token = base.GetTokenByUserId(result.Data),
                        liveurl = "",
                        playurl = ""
                    }
                };
                return data;
            });
        }
        /// <summary>
        /// 本站注册
        /// </summary>
        public HttpResponseMessage GetRegister(LoginTypeEnum loginType, string uName, string uPwd,string rePwd)
        {
            return base.Wrapper<int>(() =>
            {
                var model = new UserLoginInSide
                {
                    LoginType = (int) loginType,
                    LoginName = uName,
                    LoginPwd = uPwd
                };
                return _userLoginInSideService.Register(model, rePwd);
            });
        }
    }
}
