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
            return base.WrapperTransaction((userId) =>
            {
                var userBase = this._userBaseService.Get(m => m.ID == userId).FirstOrDefault() ?? new UserBase();
                var account = this._userAccountService.Get(m => m.UserId == userId).FirstOrDefault() ?? new UserAccount();
                var result = new Result<Object>
                {
                    Code = ResultEnum.Success,
                    Data = new
                    {
                        nickName = userBase.NickName,
                        number = userBase.UserNumber,
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
            },token);
        }
        [HttpPost]
        public HttpResponseMessage Update(string token, string nickname, string avatar, int? gender)
        {
            return base.WrapperTransaction((userId) =>
            {
                return this._userBaseService.Modify(userId, nickname, avatar, gender);
            }, token);
        }
    }
}
