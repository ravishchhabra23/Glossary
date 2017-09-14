using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GlossaryWebUI.Startup))]
namespace GlossaryWebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
