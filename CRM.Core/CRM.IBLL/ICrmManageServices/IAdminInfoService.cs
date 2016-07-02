using System.Collections.Generic;
using System.Linq;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IAdminInfoService
    {
        /// <summary>
        /// 接口约束，检查用户登录的信息
        /// </summary>
        /// <returns></returns>
        AdminInfo UserLogin(AdminInfo adminInfo);

        Result<int> DeleteUserInfo(List<int> deleteUserInfoId);

        IQueryable<AdminInfo> LoadSearchData(GetModelQuery query);

        /// <summary>
        /// 实现对用户角色的判断
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        Result<bool> SetUserInfoRole(int userId, List<int> roleIdList);

        /// <summary>
        /// 实现对用户特殊权限的设置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="actionIds"></param>
        /// <returns></returns>
        Result<bool> SetActionInfoRole(int userId, List<int> actionIds);

        Result<bool> SetAddActionInfoRole(int userId, List<int> listActionIDs);

        /// <summary>
        ///  加载所有的菜单数据，显示在表单上面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<MenuData> LoadMenuData(int userId);

    }
}
