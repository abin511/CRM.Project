using System.Collections.Generic;
using System.Linq;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    public partial class ActionInfoService : IActionInfoService
    {
        /// <summary>
        /// 实现删除多条数据
        /// </summary>
        /// <param name="actionInfoId"></param>
        /// <returns></returns>
        public Result<int> DeleteActionInfo(List<int> actionInfoId)
        {
            var result = new Result<int>();
            if (actionInfoId.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            var entities = actionInfoId.Select(m => new ActionInfo { ID = m }).ToList();
            result.Data = DbSession.ActionInfoRepository.Delete(entities);
            result.Code = ResultEnum.Error;
            return result;
        }

        /// <summary>
        /// 实现对数据的模糊查询
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        public IQueryable<ActionInfo> LoadDataActionInfo(GetModelQuery actionInfo)
        {
            //首先拿到所有的数据
            var temp = DbSession.ActionInfoRepository.Get(u => true);

            //然后进行权限名称过滤
            if (!string.IsNullOrEmpty(actionInfo.ActionName))
            {
                temp = temp.Where<ActionInfo>(c => c.ActionName.Contains(actionInfo.ActionName));
            }

            //然后进行请求方式的过滤
            if (!string.IsNullOrEmpty(actionInfo.RequestHttpType))
            {
                temp = temp.Where<ActionInfo>(c => c.RequestHttpType.Contains(actionInfo.RequestHttpType));
            }
            
            //返回总数
            actionInfo.total = temp.Count();

            //最后实现分页
            return temp.Skip<ActionInfo>(actionInfo.pageSize * (actionInfo.pageIndex - 1)).Take<ActionInfo>(actionInfo.pageSize).AsQueryable();
        }

        /// <summary>
        /// 设置权限的角色信息
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Result<bool> SetRole(int actionId, List<int> list)
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
            //首先读到用户的选中的权限ID的信息
            var currrentActionInfo = DbSession.ActionInfoRepository.Get(c => c.ID == actionId).FirstOrDefault();
            //判断是否为空
            if (currrentActionInfo == null)
            {
                result.Msg = "权限对象无效";
                return result;
            }
            //将角色项目全部给移除两个表之间的关联
            currrentActionInfo.Role.Clear();
            //在此循环便利给权限添加角色信息
            foreach (var roleID in list)
            {
                //首先查询出角色的所有的信息
                var currentRole = DbSession.RoleRepository.Get(c => c.ID == roleID).FirstOrDefault();
                currrentActionInfo.Role.Add(currentRole);
            }
            //保存设置的角色信息
            result.Data = true;
            result.Code = ResultEnum.Success;
            return result;
            //return DbSession.SaveChanges() > 0;
        }

        /// <summary>
        /// 设置权限的菜单项信息
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Result<bool> SetAction(int actionId, List<int> list)
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
            //首先根据所有的actionID读取出所有的Action信息
            var currentActionInfo = DbSession.ActionInfoRepository.Get(c => c.ID == actionId).FirstOrDefault();
            //判断得到的对象是否为空
            if (currentActionInfo == null)
            {
                result.Msg = "Action信息无效";
                return result;
            }
            //将菜单项全部移除出这两个表的观念
            currentActionInfo.ActionGroup.Clear();
            //然后循环遍历给权限添加菜单项
            foreach (var aId in list)
            {
                //首先查询出菜单项的所有信息
                var currentAction = DbSession.ActionGroupRepository.Get(c => c.ID == aId).FirstOrDefault();
                currentActionInfo.ActionGroup.Add(currentAction);
            }
            //保存设置的菜单项信息
            //return DbSession.SaveChanges() > 0;
            result.Data = true;
            result.Code = ResultEnum.Success;
            return result;
        }
    }
}
