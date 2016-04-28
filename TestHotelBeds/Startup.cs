using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestHotelBeds.Startup))]
namespace TestHotelBeds
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
