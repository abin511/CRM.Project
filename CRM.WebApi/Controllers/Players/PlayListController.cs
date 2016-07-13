using System;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebApi.Controllers.Players
{
    public class PlayListController : BaseApiController
    {
        readonly IUserBaseInfoService _userBaseInfoService = new UserBaseInfoService();
        readonly IUserAccountService _userAccountService = new UserAccountService();
        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpGet]
        public HttpResponseMessage Get(string token)
        {
            int userId = base.GetUserIdByToken(token);
            var userBase = _userBaseInfoService.Get(m => m.ID == userId).FirstOrDefault() ?? new UserBaseInfo();
            var account = _userAccountService.Get(m => m.UserId == userId).FirstOrDefault()?? new UserAccount();
            var data = new
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
                
            };
            return base.Json(data);
        }
    }
}
