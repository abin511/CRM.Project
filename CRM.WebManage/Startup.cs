using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM.WebManage.Startup))]
namespace CRM.WebManage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Help
            //ConfigureAuth(app);
        }
    }
}
