using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginOutSideService : IUserLoginOutSideService
    {
        readonly IUserBaseInfoService _userBaseInfoService = new UserBaseInfoService();
        /// <summary>
        /// 第三方用户登录，如不存在，新建用户
        /// </summary>
        public Result<bool> Login(LoginTypeEnum loginType,string openId)
        {
            var result = new Result<bool>();
            #region check params
            if (string.IsNullOrEmpty(openId))
            {
                result.Msg = "OpenId不能为空";
                return result;
            }
            if (loginType == LoginTypeEnum.Normal)
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
            }
            else
            {
                #region 插入新的用户
                var dbRet = DbSession.UserLoginOutSideRepository.Add(new UserLoginOutSide
                {
                    LoginType = (int)loginType,
                    OpenId = openId,
                    LastLoginTime = now,
                    InserTime = now,
                    UpdateTime = now
                });
                if (dbRet <= 0)
                {
                    result.Msg = "注册失败1";
                    return result;
                }
                var iRet = this._userBaseInfoService.Add(new UserBaseInfo()
                {
                    LoginId = dbRet,
                    NickName = "test",
                    RealName = "陈三",
                    UserLevel = 0,
                    Fans = 20,
                    InserTime = now,
                    UpdateTime = now
                });
                if (iRet.Code == ResultEnum.Error || iRet.Data <= 0)
                {
                    result.Msg = "注册失败2";
                    return result;
                }
                #endregion
            }
            result.Code = ResultEnum.Success;
            return result;
        }
    }
}