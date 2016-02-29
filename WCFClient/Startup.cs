using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WCFClient.Startup))]
namespace WCFClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
