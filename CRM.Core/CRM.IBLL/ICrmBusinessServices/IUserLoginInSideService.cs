using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserLoginInSideService
    {
        /// <summary>
        /// 注册用户登录 判断是否有效
        /// </summary>
        /// <returns></returns>
        Result<bool> Exists(UserLoginInSide ent);
    }
}
