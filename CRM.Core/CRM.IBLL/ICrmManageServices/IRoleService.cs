using System.Collections.Generic;
using System.Linq;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IRoleService
    {
        /// <summary>
        /// 多选删除
        /// </summary>
        /// <param name="deleteIdList"></param>
        /// <returns></returns>
        Result<int> DeleteUserRoleInfo(List<int> deleteIdList);

        /// <summary>
        /// 模糊条件的查询信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        IQueryable<Role> LoadRoleInfo(GetModelQuery roleInfo);
    }
}
