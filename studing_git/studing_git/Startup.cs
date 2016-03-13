using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(studing_git.Startup))]
namespace studing_git
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
