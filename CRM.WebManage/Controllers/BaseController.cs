using System.Web.Mvc;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class BaseController : Controller
    {
        //定义一个基类的AdminInfo对象
        public AdminInfo CurrentUserInfo => HttpContext.Session == null?new AdminInfo() : HttpContext.Session["AdminInfo"] as AdminInfo;

        /// <summary>
        /// 重写基类在Action之前执行的方法
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["AdminInfo"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
            else
            {
                if (filterContext.HttpContext.Session != null)
                {
                    var currentUserInfo = filterContext.HttpContext.Session["AdminInfo"] as AdminInfo;
                    filterContext.HttpContext.Session.Timeout = 120;
                    filterContext.HttpContext.Session["AdminInfo"] = currentUserInfo;
                }
            }
         }
    }
}
