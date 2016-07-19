using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginOutSideService : IUserLoginOutSideService
    {
        readonly IUserBaseService _userBaseInfoService = new UserBaseService();
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
            var model = DbSession.UserLoginOutSideRepository.Get(m => m.LoginType == (int)loginType && m.OpenId == openId).FirstOrDefault();
            if (model != null)
            {
                model.LastLoginTime = now;
                DbSession.UserLoginOutSideRepository.Update(model);
                result.Data = model.ID;
            }
            else
            {
                #region 插入新的用户
                var iRet1 = DbSession.UserLoginOutSideRepository.Add(new UserLoginOutSide
                {
                    LoginType = (int)loginType,
                    OpenId = openId,
                    LastLoginTime = now,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet1 <= 0)
                {
                    result.Msg = "注册失败1";
                    return result;
                }
                var iRet2 = this._userBaseInfoService.Add(new UserBase()
                {
                    LoginId = iRet1,
                    NickName = "test",
                    UserLevel = 0,
                    Fans = 0,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet2.Code == ResultEnum.Error || iRet2.Data <= 0)
                {
                    result.Msg = "注册失败2";
                    return result;
                }
                var iRet3 = DbSession.UserAccountRepository.Add(new UserAccount()
                {
                    UserId = iRet2.Data,
                    Gold = 0,
                    Contribution = 0,
                    Profit = 0,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet3 <= 0)
                {
                    result.Msg = "注册失败3";
                    return result;
                }
                result.Data = iRet2.Data;
                #endregion
            }
            result.Code = ResultEnum.Success;
            return result;
        }
    }
}