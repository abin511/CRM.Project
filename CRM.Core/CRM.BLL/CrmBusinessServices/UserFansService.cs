using System;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserFansService : IUserFansService
    {
        readonly IUserBaseService _userBaseService = new UserBaseService();
        /// <summary>
        /// 关注或取消关注某人
        /// </summary>
        /// <returns></returns>
        public Result<int> Attention(int userId, string userNumber)
        {
            var result = new Result<int>();
            var userInfo = this._userBaseService.Get(m => m.UserNumber == userNumber && m.Status == (int)UserBaseStausEnum.Normal).FirstOrDefault();
            if (userInfo == null)
            {
                result.Msg = "关注的人员无效";
                return result;
            }
            
            var fansInfo = this.CurrentRepository.Get(m => m.FansId == userId && m.UserId == userInfo.ID).FirstOrDefault();
            if (fansInfo == null)
            {
                #region 关注
                var now = DateTime.Now;
                var iRet = this.CurrentRepository.Add(new UserFans()
                {
                    UserId = userInfo.ID,
                    FansId = userId,
                    InsertTime = now,
                    UpdateTime = now
                });
                if (iRet.ID > 0)
                {
                    result.Code = ResultEnum.Success;
                    result.Data = iRet.ID;
                }
                else
                {
                    result.Msg = "关注主播信息出错";
                }
                #endregion
            }
            else
            {
                #region 取消关注
                var iRet = this.CurrentRepository.Delete(fansInfo);
                if (iRet > 0)
                {
                    result.Code = ResultEnum.Success;
                    result.Data = iRet;
                }
                else
                {
                    result.Msg = "取消关注主播信息出错";
                }
                #endregion
            }
            return result;
        }

        /// <summary>
        /// 是否关注某人
        /// </summary>
        /// <returns></returns>
        public Result<bool> IsAttention(int userId, string userNumber)
        {
            var result = new Result<bool>();
            var userInfo = this._userBaseService.Get(m => m.UserNumber == userNumber && m.Status == (int)UserBaseStausEnum.Normal).FirstOrDefault();
            if (userInfo == null)
            {
                result.Msg = "关注的人员无效";
                return result;
            }

            var fansInfo = this.CurrentRepository.Get(m => m.FansId == userId && m.UserId == userInfo.ID).FirstOrDefault();
            result.Code= ResultEnum.Success;
            result.Data = fansInfo != null && fansInfo.ID > 0;
            return result;
        }
    }
}