
using System.Web;
using System.Web.Mvc;
using chatapp.data.Model;
using chatapp.services.Implementation;
using chatapp.services.Model;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        protected string userid_cookie_name = "userid";
        protected string roomid_cookie_name = "roomid";

        private UserService _userService;

        public UserService UserService => _userService ?? (_userService = new UserService());

      
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GroupChat()
        {
            return View();
        }

        public ActionResult One2OneChat()
        {
            return View("ContactChat");
        }

            //public ActionResult One2OneChat(string pseudo, string room)
            //{
            //    HttpCookie useridCookie = Request.Cookies[userid_cookie_name];
            //    User user = null;

            //    if (useridCookie == null)
            //    {
            //        user = UserService.CreateUser(pseudo, room, UserType.One2One);
            //        useridCookie = new HttpCookie(userid_cookie_name, user.Id.ToString());
            //        var chatRoomCookie = new HttpCookie(roomid_cookie_name, user.RoomId.ToString());
            //        Response.Cookies.Add(useridCookie);
            //        Response.Cookies.Add(chatRoomCookie);
            //    }
            //    else
            //    {
            //        var userid = useridCookie.Value;
            //        user = UserService.GetUser(userid);
            //    }

            //    return View(user);
            //}
        }
}