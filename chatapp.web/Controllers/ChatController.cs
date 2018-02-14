
using System;
using System.Web.Mvc;
using chatapp.services.Implementation;
using chatapp.services.Interfaces;
using chatapp.services.Model;
using Project1.Models.ViewModel;

namespace Project1.Controllers
{
    public class ChatController : Controller
    {
        private IChatRoomService _chatRoomService;

        public IChatRoomService ChatRoomService
        {
            get
            {
                if (_chatRoomService == null)
                    _chatRoomService = new ChatRoomService();
                return _chatRoomService;
            }
        }

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

        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JoinRequestRoom(string id)
        {
            var request = ChatRoomService.GetChatRequest(new Guid(id));

            var model = new AgentChatVm
            {
                RoomId = request.RoomId.ToString(),
                ClientContextId = request.CreatorId.ToString(), 
                ClientName = request.UserName,
                RequestId = request.Id.ToString()
            }; 
            
            return View("AgentChatRequest", model);
        }
    }
}