using System;
using System.Linq;
using System.Web.Mvc;

using CRM.BLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class AuthorAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["UserInfo"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            else
            {
                var currentUserInfo = filterContext.HttpContext.Session["UserInfo"] as UserInfo;
                filterContext.HttpContext.Session.Timeout = 120;
                filterContext.HttpContext.Session["UserInfo"] = currentUserInfo;
            }
        }
    }
    [Author]
    public class BaseController : Controller
    {
        IBLL.IActionInfoService _actioninfoService = new ActionInfoService();
        IBLL.IUserInfoService _userInfoService = new UserInfoService();
        //定义一个基类的UserInfo对象
        public UserInfo CurrentUserInfo { get { return HttpContext.Session["UserInfo"] as UserInfo; } }

        /// <summary>
        /// 重写基类在Action之前执行的方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //先将当前的请求，到权限表里面去找对应的数据
            //拿到当前请求的URL地址
            string requestUrl = filterContext.HttpContext.Request.Path;
            //拿到当前请求的类型
            string requestType = filterContext.HttpContext.Request.RequestType.ToLower().Equals("get") ? "HttpGet" : "HttpPost";
            //然后和权限表进行对比，如果取出来则通过请求，否则不通过
            //取出当前权限的数据
            var currentAction = _actioninfoService.LoadEntities(c => c.RequestUrl.Equals(requestUrl, StringComparison.InvariantCultureIgnoreCase) && c.RequestHttpType.Equals(requestType)).FirstOrDefault();
            //如果没有权限对应当前请求的话，直接干掉
            if (currentAction == null)
            {
                EndRequest();
                return;
            }
            //想去用户权限表里面查询有没有数据
            //分析第一条线路 UserInfo->R_UserInfo_ActionInfo->ActionInfo
            //拿到当前的用户信息
            var userCurrent = _userInfoService.LoadEntities(u => u.ID == CurrentUserInfo.ID).FirstOrDefault();
            var temp = (from r in userCurrent.R_UserInfo_ActionInfo where r.ActionInfoID == currentAction.ID select r).FirstOrDefault();
            if (temp != null)
            {
                if (temp.HasPermation)
                {
                    return;
                }
                else
                {
                    EndRequest();
                    return;
                }
            }

            //分析第二条线路 UserInfo->ActionGroup->ActionInfo //拿到当前用户所有的组
            var groups = from n in userCurrent.ActionGroup select n;
            //根据组信息遍历出权限信息  
            bool isPass = (from g in groups from a in g.ActionInfo select a.ID).Contains(currentAction.ID);
            if (isPass)   //11，23，34不包含4
            {
                return;
            }

            //分析第三条线路 分为两个
            //1)UserInfo->R_UserInfo_Role->Role->ActionInfo

            //先拿到用户对应的所有的角色
            var UserRoles = from r in userCurrent.R_UserInfo_Role select r.Role;
            //拿到角色对应的所有权限
            var Rolesaction = (from r in UserRoles from a in r.ActionInfo select a.ID);
            if (Rolesaction.Contains(currentAction.ID))
            {
                return;
            }

            //2)UserInfo->R_UserInfo_Role->Role->ActionGroup->ActionInfo
            //拿到组信息
            var RoleGroupActions = from r in UserRoles from g in r.ActionGroup select g;
            //拿到所有的组信息
            var groupActions = from r in RoleGroupActions from g in r.ActionInfo select g.ID;
            if (groupActions.Contains(currentAction.ID))
            {
                return;
            }
        }
        public void EndRequest()
        {
            Response.Redirect("/Error.html");
        }
    }
}
