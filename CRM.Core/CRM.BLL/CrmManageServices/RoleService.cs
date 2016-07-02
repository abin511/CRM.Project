using System;
using System.Collections.Generic;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class RoleService : IRoleService
    {

        //实现删除用户的信息
        public Result<int> DeleteUserRoleInfo(List<int> deleteIdList)
        {
            var result = new Result<int>();
            if (deleteIdList.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            var entities = deleteIdList.Select(m => new Role { ID = m }).ToList();
            result.Data = DbSession.RoleRepository.Delete(entities);
            result.Code= ResultEnum.Success;
            return result;
        }

        /// <summary>
        /// 实现对查询的数据进行分组
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public IQueryable<Role> LoadRoleInfo(GetModelQuery roleInfo)
        {
            //首先查询出所有的数据
            var temp = DbSession.RoleRepository.Get(c => true);

            //判断角色名称
            if (!string.IsNullOrEmpty(roleInfo.RoleName))
            {
                temp = temp.Where<Role>(c => c.RoleName.Contains(roleInfo.RoleName));
            }

            //判断角色名称是否赋值
            if (roleInfo.RoleType != "-1" && !string.IsNullOrEmpty(roleInfo.RoleType))
            {
                temp = temp.Where<Role>(c => c.RoleType.Equals(Convert.ToInt16(roleInfo.RoleType)));
            }

            //获取总数total
            roleInfo.total = temp.Count();

            //获取总数返回
            return temp.Skip<Role>(roleInfo.pageSize * (roleInfo.pageIndex - 1)).Take(roleInfo.pageSize);
        }
    }
}
