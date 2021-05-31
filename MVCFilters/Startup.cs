using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCFilters.Startup))]
namespace MVCFilters
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
