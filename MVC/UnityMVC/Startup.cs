using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnityTutorials.Startup))]
namespace UnityTutorials
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
