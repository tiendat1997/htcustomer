using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(htcustomer.web.Startup))]
namespace htcustomer.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
