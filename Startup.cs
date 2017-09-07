using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RareVinyls.Startup))]
namespace RareVinyls
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
