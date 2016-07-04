using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserLoginOutSideService
    {
        /// <summary>
        /// 第三方登录 判断是否有效
        /// </summary>
        /// <returns></returns>
        Result<bool> Login(LoginTypeEnum loginType, string openId);
    }
}
