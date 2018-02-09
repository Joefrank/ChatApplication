using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
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

        public void connectUserToAgent(string guid, string username)
        {
            var contextId = Context.ConnectionId;
            var user = UserService.FindUserByContextId(contextId);

            var url = "~/Chat/JoinRequestRoom/" + user.RoomId;
            var roomId = user.RoomId.HasValue ? user.RoomId.Value : Guid.Empty;

            //text or email agent for them to respond.
            ChatRoomService.CreateChatRequest(user.ContextId, roomId, user.ChatPseudo, url);

        }

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