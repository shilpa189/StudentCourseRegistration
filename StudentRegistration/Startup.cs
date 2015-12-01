using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentRegistration.Startup))]
namespace StudentRegistration
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
