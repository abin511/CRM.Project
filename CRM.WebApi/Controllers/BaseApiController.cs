using System;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace CRM.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public JsonResult Json(Object data)
        {
            return new JsonResult()
            {
                Data = data,
                ContentEncoding = Encoding.UTF8,  
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
