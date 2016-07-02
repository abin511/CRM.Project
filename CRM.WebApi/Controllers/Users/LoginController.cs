using System;
using System.Web.Http;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Users
{
    public class LoginController : ApiController
    {
        readonly IUserBaseInfoService _userBaseInfoService = new UserBaseInfoService();

        [HttpGet]
        public void Login(int loginType, string openId)
        {
            var now = DateTime.Now;
            IUserLoginOutSideService service = new UserLoginOutSideService();
            var model = new UserLoginOutSide
            {
                LoginType = loginType,
                OpenId = openId,
                LastLoginTime = now,
                InserTime = now,
                UpdateTime = now
            };
            var exists = service.Exists(model);
            if (!exists.Data)
            {
                //插入用户
                var iRet = _userBaseInfoService.Add(new UserBaseInfo()
                {
                    NickName = "test",
                    RealName = "zhang",
                    Fans = 10,
                    InserTime = now,
                    UpdateTime = now
                });
                if (iRet.Code == ResultEnum.Success && iRet.Data > 0)
                {
                    model.UserId = iRet.Data;
                    service.Add(model);
                }
            }
        }
        [HttpGet]
        public void Login(int loginType, string uName, string uPwd)
        {
            IUserLoginInSideService service = new UserLoginInSideService();
            var exists = service.Exists(new UserLoginInSide { LoginName = uName, LoginPwd = uPwd });
            if (!exists.Data)
            {
                //插入用户

            }
        }
    }
}
