using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using CRM.Common;
using CRM.Model;

namespace CRM.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected HttpResponseMessage Wrapper<T>(Func<Result<T>> func)
        {
            try
            {
                var data = func();
                if (data.Code == ResultEnum.Error)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(data.Msg),
                        ReasonPhrase = "Server Error"
                    };
                    throw new HttpResponseException(response);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json")
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
