using System;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Users
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    public class ProfileController : BaseApiController
    {
        readonly IUserBaseService _userBaseService = new UserBaseService();
        readonly IUserAccountService _userAccountService = new UserAccountService();
        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpGet]
        public HttpResponseMessage Get(string token)
        {
            return base.Wrapper<Object>(() =>
            {
                int userId = base.GetUserIdByToken(token);
                var userBase = _userBaseService.Get(m => m.ID == userId).FirstOrDefault() ?? new UserBase();
                var account = _userAccountService.Get(m => m.UserId == userId).FirstOrDefault() ?? new UserAccount();
                var result = new Result<Object>
                {
                    Code = ResultEnum.Success,
                    Data = new
                    {
                        nickName = userBase.NickName,
                        avatar = userBase.Avatar,
                        level = userBase.UserLevel,
                        subScription = userBase.SubScription,
                        fans = userBase.Fans,
                        gender = userBase.Gender,
                        gold = account.Gold,
                        contribution = account.Contribution,
                        profit = account.Profit,
                    }
                };
                return result;
            });
        }
    }
}
