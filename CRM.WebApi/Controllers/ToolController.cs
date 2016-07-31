using System;
using System.Web.Mvc;
using CRM.Common;

namespace CRM.WebApi.Controllers
{
    public class ToolController:Controller
    {
        public void RefreshToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                ICacheHelper _cache = new HttpRuntimeCache();
                var cache = _cache.Get(token);
                if (cache != null)
                {
                    _cache.Add(token, cache, TimeSpan.FromMinutes(10));
                }
            }
        }
    }
}
