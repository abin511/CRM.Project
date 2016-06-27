using System;
using System.Collections.Generic;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class RoleService : BaseCrmManageService<Role>, IRoleService
    {
        #region ----由T4模版自动生成----
        //重写抽象方法，设置当前仓储为Role仓储
      
        //public override void SetCurrentRepository()
        //{
        //    //设置当前仓储来做添加
        //    //CurrentRepository = DAL.RepositoryFactory.RoleRepository;
        //    CurrentRepository = _dbSession.RoleRepository;
        //} 
        #endregion

        //实现删除用户的信息
        public int DeleteUserRoleInfo(List<int> deleteIDList)
        {
            var entities = deleteIDList.Select(m => new Role { ID = m }).ToList();
            return _dbSession.RoleRepository.Delete(entities);
        }

        /// <summary>
        /// 实现对查询的数据进行分组
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public IQueryable<Role> LoadRoleInfo(GetModelQuery roleInfo)
        {
            //首先查询出所有的数据
            var temp = _dbSession.RoleRepository.LoadEntities(c => true);

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
