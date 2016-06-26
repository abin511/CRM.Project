using System.Web.Mvc;

namespace CRM.WebManage
{
    public class CustomExceptionFilter :ActionFilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            //throw new System.NotImplementedException();
        }
    }
}