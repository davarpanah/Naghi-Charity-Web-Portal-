using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test1.Startup))]
namespace test1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
