using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginOutSideService : IUserLoginOutSideService
    {
        /// <summary>
        /// 第三方登录 判断是否有效
        /// </summary>
        /// <returns></returns>
        public Result<bool> Exists(UserLoginOutSide ent)
        {
            var result = new Result<bool>();
            if (string.IsNullOrEmpty(ent.OpenId))
            {
                result.Msg = "OpenId不能为空";
                return result;
            }
            result.Code = ResultEnum.Success;
            result.Data = DbSession.UserLoginOutSideRepository.Get(m => m.OpenId == ent.OpenId).FirstOrDefault() != null;
            return result;
        }
    }
}