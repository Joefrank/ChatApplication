using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    [HubName("GroupChatHub")]
    public class MyGroupHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void BroadCastMessage(string msgFrom, string msg, string groupName)
        {
            var id = Context.ConnectionId;
            var exceptional = new string[0];
            Clients.Group(groupName, exceptional).receiveMessage(msgFrom, msg, "");
        }

        [HubMethodName("groupconnect")]
        public void Get_Connect(string username, string userid, string connectionid, string groupName)
        {
            const string count = "NA";
            var msg = "Welcome to group " + groupName;
            const string list = "";
           
            var id = Context.ConnectionId;
            Groups.Add(id, groupName);

            var exceptional = new string[1];
            exceptional[0] = id;

            Clients.Caller.receiveMessage("Group Chat Hub", msg, list);
            Clients.OthersInGroup(groupName).receiveMessage("NewConnection", groupName + " "+username + " " + id, count);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            var clientId = Context.ConnectionId;
            var data = clientId;
            const string count = "NA";
            Clients.Caller.receiveMessage("ChatHub", data, count);
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            const string count = "NA";

            var clientId = Context.ConnectionId;
            var exceptional = new string[1];
            exceptional[0] = clientId;
            Clients.AllExcept(exceptional).receiveMessage("NewConnection", clientId + " leave", count);

            return base.OnDisconnected(stopCalled);
        }
    }
}