using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCEFCodeFirst.Startup))]
namespace MVCEFCodeFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
