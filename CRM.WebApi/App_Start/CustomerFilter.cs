using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using CRM.Common;

namespace CRM.WebApi
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogHelper.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response =actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError,new { Message = "System Exception" });
        }
    }
}