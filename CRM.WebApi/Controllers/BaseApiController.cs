using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using CRM.Common;

namespace CRM.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected HttpResponseMessage Json(Object data)
        {
            //return new JsonResult()
            //{
            //    Data = data,
            //    ContentEncoding = Encoding.UTF8,  
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //};
            //HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(userName, Encoding.GetEncoding("UTF-8"), "application/json") };
            //return result;
            
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json")
                };
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected string GetTokenByUserId(int userId)
        {
            string token = SecurityHelper.EncryptString($"{userId}|{DateTime.Now.ToUnixTimestamp()}");
            return token;
        }

        protected int GetUserIdByToken(string token)
        {
            var data = SecurityHelper.DecryptString(token).Split('|');
            return data.Length > 0 ? data[0].ToInt() : 0;
        }
    }
}
