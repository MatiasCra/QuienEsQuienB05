using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QEQB05.Startup))]
namespace QEQB05
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
