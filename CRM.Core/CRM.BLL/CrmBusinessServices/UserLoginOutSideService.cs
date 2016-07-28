using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginOutSideService : IUserLoginOutSideService
    {
        readonly IUserBaseService _userBaseInfoService = new UserBaseService();
        readonly IUserAccountService _userAccountService = new UserAccountService();
        /// <summary>
        /// 第三方用户登录，如不存在，新建用户
        /// </summary>
        public Result<int> Login(LoginTypeEnum loginType,string openId)
        {
            var result = new Result<int>();

            #region check params
            if (string.IsNullOrEmpty(openId))
            {
                result.Msg = "OpenId不能为空";
                return result;
            }
            if (loginType == LoginTypeEnum.None)
            {
                result.Msg = "登录类型无效";
                return result;
            }
            #endregion

            var now = DateTime.Now;
            var model = base.CurrentRepository.Get(m => m.LoginType == (int)loginType && m.OpenId == openId).FirstOrDefault();
            if (model != null)
            {
                model.LastLoginTime = now;
                base.CurrentRepository.Update(model);
                result.Data = model.ID;
            }
            else
            {
                #region 插入新的用户
                var iRet1 = base.CurrentRepository.Add(new UserLoginOutSide
                {
                    LoginType = (byte)loginType,
                    OpenId = openId,
                    LastLoginTime = now,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet1.ID <= 0)
                {
                    result.Msg = "注册失败1";
                    return result;
                }
                var iRet2 = this._userBaseInfoService.Add(new UserBase()
                {
                    LoginId = iRet1.ID,
                    UserNumber = string.Format(Const.UserNumber, LoginTypeEnum.M, iRet1.ToString().PadLeft(7, '0')),
                    NickName = "我是呆萌贱",
                    UserLevel = 0,
                    Fans = 0,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet2.Code == ResultEnum.Error || iRet2.Data.ID <= 0)
                {
                    result.Msg = "注册失败2";
                    return result;
                }
                var iRet3 = this._userAccountService.Add(new UserAccount()
                {
                    UserId = iRet2.Data.ID,
                    Gold = 0,
                    Contribution = 0,
                    Profit = 0,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet3.Code == ResultEnum.Error || iRet3.Data.UserId <= 0)
                {
                    result.Msg = "注册失败3";
                    return result;
                }
                result.Data = iRet2.Data.ID;
                #endregion
            }
            result.Code = ResultEnum.Success;
            return result;
        }
    }
}