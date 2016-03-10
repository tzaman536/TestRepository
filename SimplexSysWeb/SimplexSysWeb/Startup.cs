using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimplexSysWeb.Startup))]
namespace SimplexSysWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
