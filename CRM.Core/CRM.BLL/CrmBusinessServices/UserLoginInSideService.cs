using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginInSideService : IUserLoginInSideService
    {
        readonly IUserBaseInfoService _userBaseInfoService = new UserBaseInfoService();
        /// <summary>
        /// 注册用户登录
        /// </summary>
        /// <returns></returns>
        public Result<int> Login(UserLoginInSide ent)
        {
            var result = new Result<int>();
            #region check params
            if (ent.LoginType <= 0)
            {
                result.Msg = "登录类型无效";
                return result;
            }
            if (string.IsNullOrEmpty(ent.LoginName))
            {
                result.Msg = "用户名不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(ent.LoginPwd))
            {
                result.Msg = "登录密码不能为空";
                return result;
            }
            #endregion

            var now = DateTime.Now;
            var model = DbSession.UserLoginInSideRepository.Get(m => m.LoginType == ent.LoginType && m.LoginName == ent.LoginName && m.LoginPwd == ent.LoginPwd).FirstOrDefault();
            if (model == null)
            {
                //登录失败
                model = DbSession.UserLoginInSideRepository.Get(m => m.LoginType == ent.LoginType && m.LoginName == ent.LoginName).FirstOrDefault();
                if (model != null)
                {
                    model.LoginErrorCount += 1;
                    model.UpdateTime = now;
                    DbSession.UserLoginInSideRepository.Update(model);
                }
                result.Code = ResultEnum.Error;
                result.Msg = "用户名或密码错误";
            }
            else
            {
                //登录成功
                model.LoginErrorCount = 0;
                model.LastLoginTime = model.UpdateTime = now;
                DbSession.UserLoginInSideRepository.Update(model);
                result.Data = model.ID;
                result.Code = ResultEnum.Success;
            }
            return result;
        }
        /// <summary>
        /// 注册用户登录
        /// </summary>
        /// <returns></returns>
        public Result<int> Register(UserLoginInSide ent,string rePwd)
        {
            var result = new Result<int>();
            #region check params
            if (ent.LoginType <= 0)
            {
                result.Msg = "注册类型无效";
                return result;
            }
            if (string.IsNullOrEmpty(ent.LoginName))
            {
                result.Msg = "登录账号不能为空";
                return result;
            }
            if (this.Exists(ent.LoginType, ent.LoginName))
            {
                result.Msg = "登录账号已存在";
                return result;
            }
            if (string.IsNullOrEmpty(ent.LoginPwd))
            {
                result.Msg = "登录密码不能为空";
                return result;
            }
            if (ent.LoginPwd != rePwd)
            {
                result.Msg = "两次密码输入不一致";
                return result;
            }
            ent.LastLoginTime = ent.InserTime = ent.UpdateTime = DateTime.Now;
            #endregion

            var now = DateTime.Now;
            int iRet1 = DbSession.UserLoginInSideRepository.Add(ent);
            if (iRet1 <= 0)
            {
                result.Msg = "注册失败1";
                return result;
            }
            var iRet2 = this._userBaseInfoService.Add(new UserBaseInfo()
            {
                LoginId = iRet1,
                NickName = ent.LoginName,
                UserLevel = 0,
                Fans = 0,
                InserTime = now,
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
                InserTime = now,
                UpdateTime = now
            });
            if (iRet3 <= 0)
            {
                result.Msg = "注册失败3";
                return result;
            }
            result.Code = ResultEnum.Success;
            return result;
        }
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        public bool Exists(int loginType,string uName)
        {
            return DbSession.UserLoginInSideRepository.Exists(m=>m.LoginType == loginType && m.LoginName == uName);
        }
    }
}