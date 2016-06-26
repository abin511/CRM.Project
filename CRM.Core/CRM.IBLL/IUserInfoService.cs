﻿using System.Linq;
using System.Collections.Generic;
using CRM.Model;

namespace CRM.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        /// <summary>
        /// 接口约束，检查用户登录的信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        UserInfo UserLogin(UserInfo userinfo);

        int DeleteUserInfo(List<int> DeleteUserInfoID);

        IQueryable<UserInfo> LoadSearchData(GetModelQuery query);

        /// <summary>
        /// 实现对用户角色的判断
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDList"></param>
        /// <returns></returns>
        bool SetUserInfoRole(int userID, List<int> roleIDList);

        /// <summary>
        /// 实现对用户特殊权限的设置
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ActionIDS"></param>
        /// <returns></returns>
        bool SetActionInfoRole(int userID, List<int> ActionIDS);

        bool SetAddActionInfoRole(int userID, List<int> ListActionIDs);

        /// <summary>
        ///  加载所有的菜单数据，显示在表单上面
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        IQueryable<MenuData> LoadMenuData(int UserID);

    }
}
