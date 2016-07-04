using System;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Users
{
    public class LoginController : BaseApiController
    {
        /// <summary>
        /// 第三方用户登录
        /// </summary>
        [HttpGet]
        public JsonResult Login(LoginTypeEnum loginType, string openId)
        {
            var now = DateTime.Now;
            IUserLoginOutSideService service = new UserLoginOutSideService();
            var result = service.Login(loginType,openId);
            return base.Json(result);
        }
        /// <summary>
        /// 本站注册用户登录
        /// </summary>
        [HttpGet]
        public JsonResult Login(LoginTypeEnum loginType, string uName, string uPwd)
        {
            IUserLoginInSideService service = new UserLoginInSideService();
            var model = new UserLoginInSide
            {
                LoginType = (int)loginType, LoginName = uName, LoginPwd = uPwd
            };
            var result = service.Login(model);
            return base.Json(result);
        }
        /// <summary>
        /// 本站注册
        /// </summary>
        [HttpGet]
        public JsonResult Register(LoginTypeEnum loginType, string uName, string uPwd,string rePwd)
        {
            IUserLoginInSideService service = new UserLoginInSideService();
            var model = new UserLoginInSide
            {
                LoginType = (int)loginType,LoginName = uName, LoginPwd = uPwd
            };
            var result = service.Register(model,rePwd);
            return base.Json(result);
        }
    }
}
