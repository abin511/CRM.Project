﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CRM.WebManage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //TEST\]a8h
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}