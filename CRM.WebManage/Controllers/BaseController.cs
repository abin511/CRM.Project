using System.Web.Mvc;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class BaseController : Controller
    {
        //定义一个基类的UserInfo对象
        public UserInfo CurrentUserInfo => HttpContext.Session == null?new UserInfo() : HttpContext.Session["UserInfo"] as UserInfo;

        /// <summary>
        /// 重写基类在Action之前执行的方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["UserInfo"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
            else
            {
                if (filterContext.HttpContext.Session != null)
                {
                    var currentUserInfo = filterContext.HttpContext.Session["UserInfo"] as UserInfo;
                    filterContext.HttpContext.Session.Timeout = 120;
                    filterContext.HttpContext.Session["UserInfo"] = currentUserInfo;
                }
            }
         }
    }
}
