using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class UserLoginInSideService : IUserLoginInSideService
    {
        /// <summary>
        /// 第三方登录 判断是否有效
        /// </summary>
        /// <returns></returns>
        public Result<bool> Exists(UserLoginInSide ent)
        {
            var result = new Result<bool>();
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
            result.Code = ResultEnum.Success;
            result.Data = DbSession.UserLoginInSideRepository.Get(m => m.LoginName == ent.LoginName && m.LoginPwd == ent.LoginPwd).FirstOrDefault() != null;
            return result;
        }
    }
}