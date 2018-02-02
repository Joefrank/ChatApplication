using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(chatapp.web.App_Code.Startup))]
namespace chatapp.web.App_Code
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}