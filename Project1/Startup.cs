using Microsoft.Owin;
using Owin;

namespace Project1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}