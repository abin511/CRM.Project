using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserBaseService : IUserBaseService
    {
        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <returns></returns>
        public Result<int> Modify(int userId, string nickname, string avatar,int? gender)
        {
            var result = new Result<int>();
            #region check params
            if (userId <= 0)
            {
                result.Msg = "用户ID无效";
                return result;
            }
            #endregion

            var checkUser = this.CheckUser(userId);
            if (checkUser.Code == ResultEnum.Error)
            {
                result.Msg = checkUser.Msg;
                return result;
            }
            var user = checkUser.Data;
            if (!string.IsNullOrEmpty(nickname))
            {
                user.NickName = nickname;
            }
            if (!string.IsNullOrEmpty(avatar))
            {
                user.NickName = avatar;
            }
            if (gender.HasValue)
            {
                user.Gender = gender.Value;
            }
            var iRet = base.CurrentRepository.Update(user);
            result.Code = iRet > 0? ResultEnum.Success: ResultEnum.Error;
            return result;
        }
        #region CheckUser
        public Result<UserBase> CheckUser(int userId)
        {
            var result = new Result<UserBase>();
            var userInfo = this.CurrentRepository.Get(m => m.ID == userId).FirstOrDefault();
            if (userInfo == null)
            {
                result.Msg = "用户信息无效";
                return result;
            }
            if (userInfo.Status == (int)UserBaseStausEnum.Lock)
            {
                result.Msg = "用户已经被锁定";
                return result;
            }
            else if (userInfo.Status == (int)UserBaseStausEnum.Sealed)
            {
                result.Msg = "用户已经被封号";
                return result;
            }
            else
            {
                result.Data = userInfo;
                result.Code = ResultEnum.Success;
                return result;
            }
        }
        #endregion
    }
}