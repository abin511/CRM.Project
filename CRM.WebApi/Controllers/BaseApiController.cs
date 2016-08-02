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
        private readonly ICacheHelper _cache = new HttpRuntimeCache();
        protected HttpResponseMessage WrapperTransaction<T>(Func<int,Result<T>> func, string token = null)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = new TimeSpan(0, 0, 60) }))
            {
                try
                {
                    var data = func(this.GetUserId(token));
                    if (data.Code == ResultEnum.Success)
                    {
                        transactionScope.Complete();
                    }
                    return new HttpResponseMessage(data.Code == ResultEnum.Error ? HttpStatusCode.Forbidden : HttpStatusCode.OK)
                    {
                        Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json")
                    };
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
        protected string GetToken(int userId)
        {
            if (userId <= 0) return string.Empty;
            string token = SecurityHelper.EncryptString($"{userId}|{DateTime.Now.ToUnixTimestamp()}");
            _cache.Add(token, userId, TimeSpan.FromMinutes(30));
            return token;
        }
        protected int GetUserId(string token)
        {
            int userId = 0;
            if (!string.IsNullOrEmpty(token))
            {
                //根据token获取userId
                var userIdCache = _cache.Get(token) ?? 0;
                userId = int.TryParse(userIdCache.ToString(), out userId) ? userId : 0;
            }
            return userId;
        }
    }
}
