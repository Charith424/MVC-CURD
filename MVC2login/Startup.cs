using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC2login.Startup))]
namespace MVC2login
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
