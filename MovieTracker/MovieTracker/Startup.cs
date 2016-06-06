using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieTracker.Startup))]
namespace MovieTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
