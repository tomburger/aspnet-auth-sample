using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthSample.Startup))]
namespace AuthSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
