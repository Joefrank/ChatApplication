

using chatapp.data.Model;

namespace Project1.Models.ViewModel
{
    public class AgentChatVm
    {
        public string ClientContextId { get; set; }

        public string ClientName { get; set; }

        public string RoomId { get; set; }

        public User User { get; set; }
    }
}