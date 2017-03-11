using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrafficStops.Startup))]
namespace TrafficStops
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
