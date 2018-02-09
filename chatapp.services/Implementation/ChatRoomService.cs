

using System;
using System.Linq;
using chatapp.data.Model;
using chatapp.services.Interfaces;

namespace chatapp.services.Implementation
{
    public class ChatRoomService : IChatRoomService
    {
        public ChatRoom CreateRoom(string name, Guid userId)
        {
            ChatRoom room;

            using (var context = new DataDbContext())
            {
                room = new ChatRoom
                {
                    Name = name,
                    CreatorId = userId,
                    DateCreated = DateTime.Now
                };

                context.ChatRooms.Add(room);
                context.SaveChanges();
            }

            return room;
        }

        public ChatRequest CreateChatRequest(Guid creator, Guid roomId, string userName, string link)
        {
            ChatRequest request;

            using (var context = new DataDbContext())
            {
                request = new ChatRequest
                {
                    Link = link,
                    CreatorId = creator,
                    DateCreated = DateTime.Now,
                    RoomId = roomId,
                    UserName = userName
                };

                context.ChatRequests.Add(request);
                context.SaveChanges();
            }

            return request;
        }

        public ChatRequest GetChatRequest(Guid requestid)
        {
            ChatRequest request;

            using (var context = new DataDbContext())
            {
                request = context.ChatRequests.FirstOrDefault(x => x.Id == requestid);
            }

            return request;
        }
    }
}
