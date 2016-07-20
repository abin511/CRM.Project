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

            var model = base.CurrentRepository.Get(m => m.ID == userId).FirstOrDefault();
            if (model == null)
            {
                result.Msg = "用户信息无效";
                return result;
            }
            if (!string.IsNullOrEmpty(nickname))
            {
                model.NickName = nickname;
            }
            if (!string.IsNullOrEmpty(avatar))
            {
                model.NickName = avatar;
            }
            if (gender.HasValue)
            {
                model.Gender = gender.Value;
            }
            var iRet = base.CurrentRepository.Update(model);
            result.Code = iRet > 0? ResultEnum.Success: ResultEnum.Error;
            return result;
        }
    }
}