using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppServiceDemo.Startup))]
namespace AppServiceDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
