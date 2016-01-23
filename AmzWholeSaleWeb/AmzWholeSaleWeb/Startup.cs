using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmzWholeSaleWeb.Startup))]
namespace AmzWholeSaleWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
