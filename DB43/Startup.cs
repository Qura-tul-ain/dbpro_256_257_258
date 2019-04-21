using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DB43.Startup))]
namespace DB43
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
