using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Payroll.Web.Startup))]
namespace Payroll.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
