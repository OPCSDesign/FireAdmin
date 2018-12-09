using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FireAdmin.Startup))]
namespace FireAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
