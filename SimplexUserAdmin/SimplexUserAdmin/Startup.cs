using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimplexUserAdmin.Startup))]
namespace SimplexUserAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
