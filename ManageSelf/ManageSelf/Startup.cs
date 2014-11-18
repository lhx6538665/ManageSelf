using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManageSelf.Startup))]
namespace ManageSelf
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
