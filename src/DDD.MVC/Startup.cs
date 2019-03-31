using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DDD.MVC.Startup))]
namespace DDD.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
