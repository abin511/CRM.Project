using System.Web.Mvc;

namespace CRM.WebManage.Models
{
    public class MyErrorAttribute : HandleErrorAttribute
    {
        //当出现错误的时候触发此事件
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //读取exception的错误信息
            string str = filterContext.Exception.ToString();
            //记录日志
            Common.LogHelper.Error(str);
            //跳转到错误页面,全局错误信息
            filterContext.HttpContext.Response.Redirect("~/error.html");
        }
    }
}