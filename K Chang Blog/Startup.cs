using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(K_Chang_Blog.Startup))]
namespace K_Chang_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
