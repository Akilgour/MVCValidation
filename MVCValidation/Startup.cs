using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCValidation.Startup))]
namespace MVCValidation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
