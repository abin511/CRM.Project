using System;
using System.Collections.Generic;

namespace CRM.Model
{
    public class MenuData
    {
        /// <summary>
        /// 权限组的ID
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 定义MenuItem集合，菜单集合
        /// </summary>
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
    public class MenuItem
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 关联的ID
        /// </summary>
        public int Id { get; set; }
    }
}
