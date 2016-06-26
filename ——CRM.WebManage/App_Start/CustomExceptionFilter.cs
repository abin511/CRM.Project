using System.Web.Mvc;

namespace CRM.WebManage
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new System.NotImplementedException();
        }
    }
}