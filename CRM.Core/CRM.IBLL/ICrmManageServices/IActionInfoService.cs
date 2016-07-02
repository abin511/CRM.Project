using System.Collections.Generic;
using System.Linq;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IActionInfoService
    {
        //实现删除所有选择数据的接口
        Result<int> DeleteActionInfo(List<int> actionInfoId);

        //实现模糊查询的接口
        IQueryable<ActionInfo> LoadDataActionInfo(GetModelQuery actionInfo);

        //实现将权限的角色绑定到权限上面显示
        Result<bool> SetRole(int actionId, List<int> list);

        //实现将权限的菜单项绑定到权限上面
        Result<bool> SetAction(int actionId, List<int> list);
    }
}
