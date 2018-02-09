
using System;
using chatapp.data.Model;

namespace chatapp.services.Interfaces
{
    public interface IChatRoomService
    {
        ChatRoom CreateRoom(string name, Guid userId);

        ChatRequest CreateChatRequest(Guid creator, Guid roomId, string userName, string link);

        ChatRequest GetChatRequest(Guid requestid);
    }
}
