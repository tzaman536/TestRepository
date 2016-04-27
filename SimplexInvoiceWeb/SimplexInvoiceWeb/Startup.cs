using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimplexInvoiceWeb.Startup))]
namespace SimplexInvoiceWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
