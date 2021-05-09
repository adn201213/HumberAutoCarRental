using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HumberAutoCarRental.Startup))]
namespace HumberAutoCarRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
