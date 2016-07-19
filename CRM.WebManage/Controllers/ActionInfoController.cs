using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class ActionInfoController : BaseController
    {
        //首先实例化一个ActionInfo对象
        readonly IActionInfoService _actioninfoService = new ActionInfoService();
        readonly IRoleService _roleService = new RoleService();
        readonly IActionGroupService _actionGroupService = new ActionGroupService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取权限的所有的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionUserInfoShow()
        {
            //首先获取前台传递过来的参数
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            //获取前台传递过来的数据实现进行模糊查询
            string searchActionName = Request["SearchActionName"];
            string searchRequestHttpType = Request["SearchRequestHttpType"];

            //定义对象，得到所有的参数值
            GetModelQuery actionInfo = new GetModelQuery
            {
                ActionName = searchActionName,
                pageIndex = pageIndex,
                pageSize = pageSize,
                RequestHttpType = searchRequestHttpType,
                total = 0
            };

            //调用方法，实现绑定所有的数据
            var data = from c in _actioninfoService.LoadDataActionInfo(actionInfo)
                       select new { c.ID, c.ActionName, c.ActionType, c.RequestUrl, c.RequestHttpType, c.SubTime };
            //var data = _actioninfoService.List(pageSize, pageIndex, out total, c => true, true, e => e.id);
            //获取前台需要的数据
            var jsonResult = new { total = actionInfo.total, rows = data };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加用户权限信息
        /// </summary>
        /// <param name="actioninfo"></param>
        /// <returns></returns>
        public ActionResult AddActionInfo(ActionInfo actioninfo)
        {
            actioninfo.SubTime = DateTime.Now;
            var result = _actioninfoService.Add(actioninfo);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 绑定用户权限问题
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult BindActionInfo(int ID)
        {
            var jsonData = _actioninfoService.Get(c => c.ID == ID).FirstOrDefault();
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 对用户权限信息进行修改
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        public ActionResult UpdateActionInfo(ActionInfo actionInfo)
        {
            //首先查询出所有的用户权限信息
            ActionInfo editActionInfo = _actioninfoService.Get(c => c.ID == actionInfo.ID).FirstOrDefault();
            if (editActionInfo == null)
            {
                return Content("修改错误，请您检查");
            }
            //给要修改的实体对象赋值
            editActionInfo.ActionName = actionInfo.ActionName;
            editActionInfo.RequestHttpType = actionInfo.RequestHttpType;
            editActionInfo.RequestUrl = actionInfo.RequestUrl;
            editActionInfo.ActionType = actionInfo.ActionType;
            //进行修改信息
            var result = _actioninfoService.Update(editActionInfo);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 多选删除用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteActionInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return Content("请选择您要删除的数据");
            }
            var deleteId = ID.Split(',');

            //定义List集合存放这些需要删除的数据
            List<int> list = deleteId.Select(id => Convert.ToInt32(id)).ToList();

            //实现删除多条数据的方法
            var result = _actioninfoService.DeleteActionInfo(list);
            if (result.Code == ResultEnum.Success && result.Data > 0)
            {
                return Content("OK");
            }
            return Content(result.Msg);
        }

        /// <summary>
        /// 实现设置权限角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRole(int id)
        {
            //首先根据传递过来的ID找到权限信息
            var currrentSetRoleAction = _actioninfoService.Get(u => u.ID == id).FirstOrDefault();
            ViewData.Model = currrentSetRoleAction;

            //传递过去前台需要用到的数据,遍历角色，显示出来
            short RoleNomal = (short)DelFlagEnum.None;
            var allRoles = _roleService.Get(c => c.DelFlag == RoleNomal).ToList();
            ViewBag.AllRoles = allRoles;

            //传递给前台权限对应的角色信息，方便用户给角色设置权限
            ViewBag.ExistRoleIds = (from n in currrentSetRoleAction.Role  //权限和角色表的中间表的数据
                                    select n.ID).ToList();

            return View();
        }

        /// <summary>
        /// 处理权限的的请求，添加角色信息，
        /// </summary>
        /// <returns></returns>
        [HttpPost ]
        public ActionResult SetRole()
        {
            //首先读取到前台传递过来的权限ID
            int actionId = Request["hidenActionID"] == null ? 0 : Convert.ToInt32(Request["hidenActionID"]);
            //查询出当前权限的所有的信息,根据Action
            var currentActionInfo = _actioninfoService.Get(c => c.ID == actionId).FirstOrDefault();
            if (currentActionInfo != null)
            {
                //判断如果权限信息不为空的话执行，拿到前台表单传递的信息，进行操作，ckb_1,ckb_2,ckb_3
                var allKeys = from key in Request.Form.AllKeys
                              where key.Contains("ckb_")
                              select key;
                //定义一个List集合存放传递过来的Key
                List<int> list = new List<int>();
                if (actionId > 0)
                {
                    //循环便利所有的前台的信息展示给用户显示
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("ckb_", "")));
                    }
                }
                var result = _actioninfoService.SetRole(actionId, list);
                if (result.Code == ResultEnum.Success && result.Data)
                {
                    return Content("OK");
                }
                return Content(result.Msg);
            }
            return Content("无效的数据");
        }

        /// <summary>
        /// 获取设置用户分组的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetAction(int id)
        {
            //根据ID查询出权限信息的信息
            var currentUser = _actioninfoService.Get(u => u.ID == id).FirstOrDefault();
            ViewData.Model = currentUser;

            //根据传递过去的菜单组获取到所有的菜单组信息
            short ActionID = (short)DelFlagEnum.None;
            var allAction = _actionGroupService.Get(c => c.DelFlag == ActionID).ToList();
            ViewBag.AllAction = allAction; 
            //然后传递给前台判断权限组数据是否被选中
            ViewBag.Exists = (from n in currentUser.ActionGroup  //获取到权限表
                           select n.ID).ToList();
            return View();
        }

        /// <summary>
        /// 对权限里面的权限分组设置菜单组的操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetAction()
        {
            //首先根据前台传递过来的隐藏域得到actionID
            int actionId = Request["hidenActionID"] == null ? 0 : Convert.ToInt32(Request["hidenActionID"]);
            //根据actionID查询出来当前权限对应的ID
            var currentActionInfo = _actioninfoService.Get(c => c.ID == actionId).FirstOrDefault();
            if (currentActionInfo != null)
            {
                //拿到前台表单传递的表单选中值，形势为act_1，act_2,act_3
                var allKeys = from key in Request.Form.AllKeys
                              where key.Contains("act_")
                              select key;
                //定义一个集合用来存放传递过来的Key
                List<int> list = new List<int>();
                //循环遍历得到所有的前台数据显示在这里
                if (actionId > 0)
                {
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("act_", "")));
                    }
                }
                var result = _actioninfoService.SetAction(actionId, list);
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
