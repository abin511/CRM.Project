using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

namespace CRM.BLL
{
    /// <summary>
    /// AdminInfo相关的业务封装
    /// </summary>
    public partial class AdminInfoService : IAdminInfoService
    {
        ////访问DAL实现CRUD，记得这里使用接口编程：IAdminInfoRepository,不要写成AdminInfoRepository
        ////依赖接口编程
        ////private IAdminInfoRepository _adminInfoRepository = new AdminInfoRepository();
        ////当AdminInfoRepository实例变化的时候，在BLL很多地方都用到了此实例，

        ////简单工厂：创建实例，不再依赖具体的实例的类型
        //private IAdminInfoRepository _adminInfoRepository = RepositoryFactory.AdminInfoRepository;

        //public AdminInfo AddAdminInfo(AdminInfo adminInfo)
        //{
        //    return _adminInfoRepository.AddEntities(adminInfo);
        //}

        /// <summary>
        /// 检验用户是否登录成功
        /// </summary>
        /// <returns></returns>
        public AdminInfo UserLogin(AdminInfo adminInfo)
        {
            //判断用户的用户名密码是否正确
            return DbSession.AdminInfoRepository.Get(u => u.UName == adminInfo.UName && u.Pwd == adminInfo.Pwd).FirstOrDefault();
        }

        /// <summary>
        /// List集合实现多选删除数据
        /// </summary>
        /// <param name="deleteUserInfoId"></param>
        /// <returns></returns>
        public Result<int> DeleteUserInfo(List<int> deleteUserInfoId)
        {
            //foreach (var ID in deleteUserInfoId)
            //{
            //    DbSession.UserInfoRepository.DeleteEntities(new AdminInfo() { ID = ID });
            //}
            //return DbSession.SaveChanges();
            var result = new Result<int>();
            if (deleteUserInfoId.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            var entities = deleteUserInfoId.Select(m => new AdminInfo { ID = m }).ToList();
            result.Data = DbSession.AdminInfoRepository.Delete(entities);
            result.Code = ResultEnum.Success;
            return result;
        }

        /// <summary>
        /// 加载用户模糊查询的数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<AdminInfo> LoadSearchData(GetModelQuery query)
        {
            //拿到所有的数据
            var temp = DbSession.AdminInfoRepository.Get(u => true);

            //进行过滤姓名
            if (!string.IsNullOrEmpty(query.Name))
            {
                temp = temp.Where<AdminInfo>(u => u.UName.Contains(query.Name));
            }
            //进行邮箱的过滤
            if (!string.IsNullOrEmpty(query.Mail))
            {
                temp = temp.Where<AdminInfo>(u => u.Mail.Contains(query.Mail));
            }
            //返回总数
            query.total = temp.Count();

            //做分页查询
            return temp.Skip(query.pageSize * (query.pageIndex - 1)).Take(query.pageSize).AsQueryable();

        }

        /// <summary>
        /// 实现添加用户角色的信息，先删除所有的数据，然后再次的添加新数据就行了
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        public Result<bool> SetUserInfoRole(int userId, List<int> roleIdList)
        {
            var result = new Result<bool>();
            if (userId <= 0)
            {
                result.Msg = "userId无效";
                return result;
            }
            if (roleIdList.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            //首先根据用户ID，查询出用户的信息
            var currentUser = DbSession.AdminInfoRepository.Get(c => c.ID == userId).FirstOrDefault();
            if (currentUser == null)
            {
                result.Msg = "当前用户信息无效";
                return result;
            }
            //得到角色表中的数据全部返回
            var listRoles = currentUser.R_AdminInfo_Role.ToList();
            //处理清空原来的数据，用户的和角色的中间表信息
            //for (int i = 0; i < listRoles.Count; i++)
            //{
            //    DbSession.R_UserInfo_RoleRepository.DeleteEntities(listRoles[i]);
            //}
            ////真正的删除了所有的数据
            //DbSession.SaveChanges();

            //var entities = deleteUserInfoId.Select(m => new AdminInfo { ID = m }).ToList();
            DbSession.R_AdminInfo_RoleRepository.Delete(listRoles);

            listRoles = roleIdList.Select(roleId => new R_AdminInfo_Role()
            {
                RoleID = roleId, UserInfoID = userId, SubTime = DateTime.Now
            }).ToList();
            //在此重新将数据加载会数据库
            //实现添加功能
            //DbSession.SaveChanges();
            result.Data = DbSession.R_AdminInfo_RoleRepository.Add(listRoles) > 0;
            result.Code = ResultEnum.Success;
            return result;
        }

        public Result<bool> SetActionInfoRole(int userId, List<int> actionIds)
        {
            var result = new Result<bool>();
            if (userId <= 0)
            {
                result.Msg = "userId无效";
                return result;
            }
            if (actionIds.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            //根据ID,查询出用户的所有的信息
            var currentUser = DbSession.AdminInfoRepository.Get(c => c.ID == userId).FirstOrDefault();
            if (currentUser == null)
            {
                result.Msg = "当前用户信息无效";
                return result;
            }
            //得到权限表中的所有数据返回
            var actionList = currentUser.R_AdminInfo_ActionInfo.ToList();
            //循环遍历删除所有的用户的权限信息
            //for (int i = 0; i < actionList.Count; i++)
            //{
            //    DbSession.R_UserInfo_ActionInfoRepository.DeleteEntities(actionList[i]);
            //}
            //DbSession.SaveChanges();
            DbSession.R_AdminInfo_ActionInfoRepository.Delete(actionList);
            //将所有的新的数据在此的加入到表中
            actionList = actionIds.Select(actionId => new R_AdminInfo_ActionInfo()
            {
                UserInfoID = userId, ActionInfoID = actionId, HasPermation = true,
            }).ToList();
            result.Data = DbSession.R_AdminInfo_ActionInfoRepository.Add(actionList) > 0;
            result.Code = ResultEnum.Success;
            return result;
        }

        /// <summary>
        /// 添加用户特殊权限的设置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listActionIDs"></param>
        /// <returns></returns>
        public Result<bool> SetAddActionInfoRole(int userId, List<int> listActionIDs)
        {
            var result = new Result<bool>();
            if (userId <= 0)
            {
                result.Msg = "userId无效";
                return result;
            }
            if (listActionIDs.Count <= 0)
            {
                result.Msg = "无效的选择";
                return result;
            }
            //首先根据用户ID查询到用户的信息
            var currentUser = DbSession.AdminInfoRepository.Get(c => c.ID == userId).FirstOrDefault();
            if (currentUser == null)
            {
                result.Msg = "当前用户信息无效";
                return result;
            }
            //根据用户信息得到权限表的信息显示出来
            var actionInfo = currentUser.R_AdminInfo_ActionInfo.ToList();
            DbSession.R_AdminInfo_ActionInfoRepository.Delete(actionInfo);

            //然后将选择的数据在添加到信息中
            actionInfo = new List<R_AdminInfo_ActionInfo>();
            foreach (var actionID in listActionIDs)
            {
                actionInfo.Add(new R_AdminInfo_ActionInfo()
                {
                    UserInfoID = userId,
                    ActionInfoID = actionID,
                    HasPermation = true
                });
            }
            result.Data = DbSession.R_AdminInfo_ActionInfoRepository.Add(actionInfo) > 0;
            result.Code = ResultEnum.Success;
            return result;
        }

        /// <summary>
        /// 加载所有的菜单数据，显示在表单上面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<MenuData> LoadMenuData(int userId)
        {
            //先获取到UserID的用户信息
            var CurrentUser = DbSession.AdminInfoRepository.Get(c => c.ID == userId).FirstOrDefault();
            //判断是否为空
            if (CurrentUser == null)
            {
                return null;
            }
            //根据用户拿到对应的角色
            var userRoleList = from r in CurrentUser.R_AdminInfo_Role select r.Role;
            //根据角色对应的分组
            var groups = from n in userRoleList from g in n.ActionGroup select g;

            //获取选中的是菜单项的选择
            short actionTypeMenu = (short)ActionTypeEnum.MenuItem;

            //实现过滤重复的数据，引用不同
            //默认的就是引用类型，对比的时候用的是引用类型，如果我们不想使用引用地址，而人为指定表的属性，那么可以自己写一个比较起，重写Equals和GethashCode方法就行了
            groups.Distinct(new UtilityHelper.EntityCompare());

            //把所有的信息封装MenuData数据传递给控制器，Json格式
            var menuData = from g in groups
                           select new MenuData()
                           {
                               GroupID = g.ID,
                               GroupName = g.GroupName,
                               MenuItems = (from a in g.ActionInfo where a.ActionType == actionTypeMenu
                                            select new MenuItem
                                            {
                                                Id = a.ID,
                                                MenuName = a.ActionName,
                                                Url = a.RequestUrl
                                            })
                           };
            return menuData.AsQueryable();
        }
    }
}