using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class RoleController : BaseController
    {
        readonly IRoleService _roleService = new RoleService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 实现对用户角色的绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserRoleInfo()
        {
            //首先获取从前台传递过来的参数
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            //获取从前台传递过来的需要多条件模糊查询的数据
            string roleName = Request["RoleName"];
            string roleType = Request["RoleType"];

            //定义对象，得到所有的参数
            GetModelQuery roleInfo = new GetModelQuery();
            roleInfo.pageIndex = pageIndex;
            roleInfo.pageSize = pageSize;
            roleInfo.total = 0;
            roleInfo.RoleName = roleName;
            roleInfo.RoleType = roleType;

            //获取所有的总数输入
            var data = from n in _roleService.LoadRoleInfo(roleInfo)
                       select new { n.ID, n.DelFlag, n.RoleName, n.RoleType, n.SubTime };
            //var data = _roleService.List(pageSize, pageIndex, out total, c => true, true, d => d.id);
            var value = data.ToList();
            var jsonResult = new { total = roleInfo.total, rows = value };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现对用户角色的添加信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult AddUserRoleInfo(Role role)
        {
            //实现对用户的添加信息
            role.DelFlag = (short)DelFlagEnum.None;
            role.SubTime = DateTime.Now;
            var result = _roleService.Add(role);
            if (result.Code == ResultEnum.Success && result.Data.ID > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 实现对用户角色的绑定信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BindUserRoleInfo(int id)
        {
            var bindUserRoleInfoJson = _roleService.Get(c => c.ID == id).FirstOrDefault();

            return Json(bindUserRoleInfoJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public ActionResult UpdateUserRoleInfo(Role roleInfo)
        {
            //查询出Role实体对象
            var editRole = _roleService.Get(c => c.ID == roleInfo.ID).FirstOrDefault();

            //查询出实体对象然后修改
            editRole.RoleName = roleInfo.RoleName;
            editRole.RoleType = roleInfo.RoleType;
            var result = _roleService.Update(editRole);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUserRoleInfo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("请选择您要删除的数据");
            }

            //截取传递过来的字符串显示成数组形式
            var deleteId = id.Split(',');

            //定义数组存放删除的ID
            List<int> deleteIdList = deleteId.Select(m => Convert.ToInt32(m)).ToList();
            var result = _roleService.DeleteUserRoleInfo(deleteIdList);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }
    }
}
