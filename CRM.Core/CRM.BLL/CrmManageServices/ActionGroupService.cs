using System;
using System.Collections.Generic;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class ActionGroupService :IActionGroupService
    {
        public IQueryable<ActionGroup> LoadEntityActionGroup(GetModelQuery actionGroup)
        {
            //首先读取到所有的数据
            var temp = DbSession.ActionGroupRepository.Get(c => true);

            //根据菜单组名称进行过滤
            if (!string.IsNullOrEmpty(actionGroup.GroupName))
            {
                temp = temp.Where<ActionGroup>(c => c.GroupName.Contains(actionGroup.GroupName));
            }
            //根据菜单组类型进行过滤
            if (actionGroup.GroupType != "-1" && !string.IsNullOrEmpty(actionGroup.GroupType))
            {
                temp = temp.Where<ActionGroup>(c => c.GroupType.Equals(Convert.ToInt16(actionGroup.GroupType)));
            }
            //得到菜单组的总数
            actionGroup.total = temp.Count();

            //进行分页查询信息
            return temp.Skip<ActionGroup>(actionGroup.pageSize * (actionGroup.pageIndex - 1)).Take(actionGroup.pageSize);
        }

        /// <summary>
        /// 实现对权限组的信息的删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Result<int> DeleteSetActionGroupInfo(List<int> list)
        {
            var result = new Result<int>();
            if (list.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            var entities = list.Select(m => new ActionGroup { ID = m }).ToList();
            result.Data = DbSession.ActionGroupRepository.Delete(entities);
            result.Code = ResultEnum.Success;
            return result;
        }

        /// <summary>
        /// 设置菜单项的角色信息
        /// </summary>
        /// <returns></returns>
        public Result<bool> setRole(int actionId, List<int> list)
        {
            var result = new Result<bool>();
            if (actionId <= 0)
            {
                result.Msg = "actionId无效";
                return result;
            }
            if (list.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            //首先根据actionId查询出菜单组的对象
            var actionGroupInfoShow = DbSession.ActionGroupRepository.Get(c => c.ID == actionId).FirstOrDefault();
            //判断得到的对象是否为空
            if (actionGroupInfoShow == null)
            {
                result.Msg = "菜单组的对象无效";
                return result;
            }
            //删除以前的旧数据
            actionGroupInfoShow.Role.Clear();

            //然后将List集合循环遍历添加到项目中即可
            foreach (var roleId in list)
            {
                //首先查询出角色ID的信息
                var roleInfo = DbSession.RoleRepository.Get(c => c.ID == roleId).FirstOrDefault();
                //实现添加
                actionGroupInfoShow.Role.Add(roleInfo);
            }
            result.Data = true;
            result.Code= ResultEnum.Success;
            return result;
        }
    }
}
