using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WordBrainSolver.API.Startup))]
namespace WordBrainSolver.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
