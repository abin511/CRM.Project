using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class ActionGroupController : BaseController
    {
        readonly IActionGroupService _actiongroupService = new ActionGroupService();
        readonly IRoleService _roleServices = new RoleService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 读取用户所有的权限组的信息显示在前台
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllActionGroupInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string groupName = Request["SearchActionName"];
            string groupType = Request["SearchActionType"];

            GetModelQuery actionGroupInfo = new GetModelQuery();
            actionGroupInfo.pageIndex = pageIndex;
            actionGroupInfo.pageSize = pageSize;
            actionGroupInfo.total = 0;
            actionGroupInfo.GroupName = groupName;
            actionGroupInfo.GroupType = groupType;

            // var data = _actiongroupService.List(pageSize, pageIndex, out total, c => true, true, c => c.id);
            var data = from x in _actiongroupService.LoadEntityActionGroup(actionGroupInfo)
                       select new { x.ID, x.DelFlag, x.GroupName, x.GroupType };
            var postJson = new { total = actionGroupInfo.total, rows = data };

            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现添加菜单组的详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddActionGroupInfo(ActionGroup actionGroup)
        {
            actionGroup.DelFlag = 0;
            var result = _actiongroupService.Add(actionGroup);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 实现对前台信息的绑定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BindActionGroupInfo(int id)
        {
            var data = _actiongroupService.Get(c => c.ID == id).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现对菜单组的修改工作
        /// </summary>
        /// <param name="actionGroup"></param>
        /// <returns></returns>
        public ActionResult UpdateActionGroup(ActionGroup actionGroup)
        {
            //首先查询出所有的actionInfo的单个信息根据ID
            var editorActionInfo = _actiongroupService.Get(c => c.ID == actionGroup.ID).FirstOrDefault();
            //获取要删除的数据
            editorActionInfo.GroupName = actionGroup.GroupName;
            editorActionInfo.GroupType = actionGroup.GroupType;

            var result = _actiongroupService.Update(editorActionInfo);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 解析删除权限组的信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteActionGroupInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return Content("请选择您要删除的数据");
            }
            var deleteID = ID.Split(',');
            //定义数组存放需要删除的ID
            List<int> list = new List<int>();
            foreach (var Dsid in deleteID)
            {
                list.Add(Convert.ToInt32(Dsid));
            }
            //然后执行删除的方法删除数据
            var result = _actiongroupService.DeleteSetActionGroupInfo(list);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 获取角色信息显示在前台页面中
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult SetRole(int ID)
        {
            //首先根据ID,查询出菜单组的所有的信息
            var actionGroup = _actiongroupService.Get(c => c.ID == ID).FirstOrDefault();
            ViewData.Model = actionGroup;

            //然后查询出所有的角色信息显示在前台
            short RoleID = (short)DelFlagEnum.None;
            var allRoleInfo = _roleServices.Get(c => c.DelFlag == RoleID).ToList();
            ViewBag.RoleInfo = allRoleInfo;

            //判断此角色是否被选择
            ViewBag.exists = (from n in actionGroup.Role  //菜单项和角色表的中间项
                              select n.ID).ToList();


            return View();
        }

        /// <summary>
        /// 对菜单项的角色的添加信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRole()
        {
            //根据前台隐藏字段传递过来菜单项的ID信息
            int GroupID = Request["HidenID"] == null ? 0 : Convert.ToInt32(Request["HidenID"]);
            //查询出菜单项中的GroupID的数据
            var GroupInfo = _actiongroupService.Get(c => c.ID == GroupID).FirstOrDefault();
            if (GroupInfo != null)
            {
                //判断如果菜单项不为空的话，读取前台的所有的信息
                var allKeys = from n in Request.Form.AllKeys
                              where n.StartsWith("al_")
                              select n;
                //定义一个集合存放传递过来的key
                List<int> list = new List<int>();
                if (GroupID > 0)
                {
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("al_", "")));
                    }
                }
                var result = _actiongroupService.setRole(GroupID, list);
                if (result.Code == ResultEnum.Success && result.Data)
                {
                    return Content("OK");
                }
                return Content(result.Msg);
            }
            return Content("无效的数据");
        }
    }
}
