using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeneralStore116.Startup))]
namespace GeneralStore116
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
