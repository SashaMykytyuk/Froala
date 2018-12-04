using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestIIS.Startup))]
namespace TestIIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
