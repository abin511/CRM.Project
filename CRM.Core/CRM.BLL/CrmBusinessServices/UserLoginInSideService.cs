using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginInSideService : IUserLoginInSideService
    {
        readonly IUserBaseService _userBaseInfoService = new UserBaseService();
        readonly IUserAccountService _userAccountService = new UserAccountService();
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
            var loginModel = base.CurrentRepository.Get(m => m.LoginType == ent.LoginType && m.LoginName == ent.LoginName && m.LoginPwd == ent.LoginPwd).FirstOrDefault();
            if (loginModel == null)
            {
                //登录失败
                loginModel = base.CurrentRepository.Get(m => m.LoginType == ent.LoginType && m.LoginName == ent.LoginName).FirstOrDefault();
                if (loginModel != null)
                {
                    loginModel.LoginErrorCount += 1;
                    loginModel.UpdateTime = now;
                    base.CurrentRepository.Update(loginModel);
                }
                result.Code = ResultEnum.Error;
                result.Msg = "用户名或密码错误";
            }
            else
            {
                var userInfo = this._userBaseInfoService.Get(m => m.LoginId == loginModel.ID).FirstOrDefault();
                if (userInfo == null)
                {
                    result.Msg = "用户基础信息无效";
                    return result;
                }

                //登录成功
                loginModel.LoginErrorCount = 0;
                loginModel.LastLoginTime = loginModel.UpdateTime = now;
                base.CurrentRepository.Update(loginModel);
                result.Data = userInfo.ID;
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
            ent.LastLoginTime = ent.InsertTime = ent.UpdateTime = DateTime.Now;
            #endregion

            var now = DateTime.Now;
            var iRet1 = base.CurrentRepository.Add(ent);
            if (iRet1.ID <= 0)
            {
                result.Msg = "注册失败1";
                return result;
            }
            var iRet2 = this._userBaseInfoService.Add(new UserBase()
            {
                LoginId = iRet1.ID,
                UserNumber = string.Format(Const.UserNumber, LoginTypeEnum.M,iRet1.ToString().PadLeft(7, '0')),
                NickName = ent.LoginName,
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
            result.Code = ResultEnum.Success;
            return result;
        }
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        public bool Exists(int loginType,string uName)
        {
            return base.CurrentRepository.Exists(m=>m.LoginType == loginType && m.LoginName == uName);
        }
    }
}