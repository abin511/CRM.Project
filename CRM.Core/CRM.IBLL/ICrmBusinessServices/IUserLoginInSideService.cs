using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserLoginInSideService
    {
        /// <summary>
        /// 注册用户登录
        /// </summary>
        /// <returns></returns>
        Result<int> Login(UserLoginInSide ent);
        /// <summary>
        /// 本站用户注册
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="rePwd"></param>
        /// <returns></returns>
        Result<int> Register(UserLoginInSide ent, string rePwd);
    }
}
