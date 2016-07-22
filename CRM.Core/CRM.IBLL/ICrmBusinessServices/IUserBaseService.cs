using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserBaseService
    {
        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <returns></returns>
        Result<int> Modify(int userId, string nickname, string avatar, int? gender);

        Result<UserBase> CheckUser(int userId);
    }
}
