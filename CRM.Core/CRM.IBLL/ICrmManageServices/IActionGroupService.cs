using System.Collections.Generic;
using System.Linq;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IActionGroupService
    {
        /// <summary>
        /// 实现模糊查询的接口
        /// </summary>
        /// <param name="actionGroup"></param>
        /// <returns></returns>
        IQueryable<ActionGroup> LoadEntityActionGroup(GetModelQuery actionGroup);

        /// <summary>
        /// 实现对权限组的删除功能
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Result<int> DeleteSetActionGroupInfo(List<int> list);

        /// <summary>
        /// 添加菜单项的角色信息
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        Result<bool> setRole(int actionId, List<int> list);
    }
}
