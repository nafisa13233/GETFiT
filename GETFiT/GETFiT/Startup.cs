using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GETFiT.Startup))]
namespace GETFiT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
