using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using chatapp.data.Model;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Project1
{
    [HubName("chatRoom")]
    public class MyHub : Hub
    {
        [HubMethodName("publish")]
        public void Announce(string message)
        {
            if (message.StartsWith("¬¬NEW_CONNECTION¬¬"))
            {
                var arr = message.Split('¦');
                Clients.All.DisplayWelcomeMessage("<b>" + arr[1] + "</b> Joined chat at " + DateTime.Now.ToString("dd MMM yyyy @ hh:mm"));
                
                return;
            }
            Clients.All.DisplayClientMessage("<br/>" + message);
        }

        //public override Task OnConnected()
        //{
        //    var temp = Clients.Caller.ToString();
        //    return base.OnConnected();
        //}

        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;
            var useragent = Context.Request.Headers["User-Agent"];
           
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }
    }
}