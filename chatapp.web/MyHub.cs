using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using chatapp.data.Model;
using chatapp.services.Implementation;
using chatapp.services.Interfaces;
using chatapp.services.Model;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Project1
{
    [HubName("chatRoom")]
    public class MyHub : Hub
    {
        private IUserService _userService;

        public IUserService UserService
        {
            get
            {
                if (_userService == null)
                    _userService = new UserService();

                return _userService;
            }
        }

        private IChatRoomService _chatRoomService;

        public IChatRoomService ChatRoomService
        {
            get
            {
                if(_chatRoomService == null)
                    _chatRoomService = new ChatRoomService();
                return _chatRoomService;
            }
        }

        [HubMethodName("connectUserToHub")]
        public void ConnectUserToRoom(string username)
        {
            var contextId = Context.ConnectionId;

            //check that username is not empty
            if (string.IsNullOrEmpty(username))
            {
                Clients.Caller.handleServerError(1, "You need to provide a pseudo in order to enter the chatroom.");
                return;
            }

            try
            {
                //check if this user exists
                var user = UserService.FindUserByContextIdAndName(username, contextId);

                //if user is null, create new user and his unique chatroom
                if (user == null)
                {
                    var roomName = username + contextId;
                    user = UserService.CreateUser(username, roomName, UserType.One2One, new Guid(contextId));
                    var userRoomId = user.RoomId ?? Guid.Empty;
                    Clients.Caller.receiveConnectionDetails(user.Id, userRoomId);
                }
                else
                {
                    //get previous messages (this case should normally not happen)
                }
            }
            catch (Exception d)
            {
                //log here.
                Clients.Caller.handleServerError(1, "Sorry an error occured.");
                
            }
        }

        [HubMethodName("connectAgentToHub")]
        public void ConnectAgentToHub(string agentname, Guid roomId, Guid requestId)
        {
            var contextId = Context.ConnectionId;

            //check that username is not empty
            if (string.IsNullOrEmpty(agentname))
            {
                Clients.Caller.handleServerError(1, "You need to provide a pseudo in order to enter the chatroom.");
                return;
            }

            try
            {
                //check if this user exists
                var user = UserService.FindAgent(agentname, roomId);

                //if user is null, create new user and his unique chatroom
                if (user == null)
                {
                    user = UserService.CreateAgent(agentname, UserType.One2OneAdmin, new Guid(contextId), roomId, requestId);
                    Clients.Caller.receiveConnectionDetails(user.Id);
                }
                else
                {
                    //get previous messages (this case should normally not happen)
                }
            }
            catch (Exception d)
            {
                //log here.
                Clients.Caller.handleServerError(1, "Sorry an error occured.");

            }
        }

        [HubMethodName("requestAgentConnection")]
        public void RequestAgentConnection(string guid, string username)
        {
            var contextId = Context.ConnectionId;
            var user = UserService.FindUserByContextId(contextId);

            var url = ConfigurationManager.AppSettings["BaseUrl"] + "/Chat/JoinRequestRoom/" + user.RoomId;
            var roomId = user.RoomId.HasValue ? user.RoomId.Value : Guid.Empty;

            //text or email agent for them to respond.
            ChatRoomService.CreateChatRequest(user.ContextId, roomId, user.ChatPseudo, url);

        }
        [HubMethodName("announceAgentToClients")]
        public void AnnounceAgentToClients(Guid userid)
        {
            var user = UserService.GetUser(userid);
            Clients.AllExcept(user.ContextId.ToString()).acknowledgeAgentConnection(user.ChatPseudo);
        }

        [HubMethodName("broadcastMessage")]
        public void BroadcastMessage(string message, string sender)
        {
            Clients.All.displayClientMessage(message, sender);
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