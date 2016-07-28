using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Web.Http;
using CRM.Common;
using CRM.Model;

namespace CRM.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected HttpResponseMessage WrapperTransaction<T>(Func<Result<T>> func)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = new TimeSpan(0, 0, 60) }))
            {
                try
                {
                    var data = func();
                    if (data.Code == ResultEnum.Success)
                    {
                        transactionScope.Complete();
                    }
                    return Response(data);
                }
                catch (Exception ex)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(ex.Message),
                        ReasonPhrase = "Server Error"
                    };
                    throw new HttpResponseException(response);
                }
                finally
                {
                    transactionScope.Dispose();
                }
            }
        }
        protected HttpResponseMessage WrapperResponse<T>(Func<Result<T>> func)
        {
            try
            {
                var data = func();
                return Response(data);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = "Server Error"
                };
                throw new HttpResponseException(response);
            }
        }

        private HttpResponseMessage Response<T>(Result<T> data)
        {
            if (data.Code == ResultEnum.Error)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(data.Msg, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json")
                };
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
