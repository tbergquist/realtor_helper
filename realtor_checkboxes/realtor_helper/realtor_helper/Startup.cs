using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(realtor_helper.Startup))]
namespace realtor_helper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth(app);
        }
    }
}
