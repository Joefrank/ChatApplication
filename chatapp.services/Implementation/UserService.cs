using System;
using System.Linq;
using chatapp.data.Model;
using chatapp.services.Interfaces;
using chatapp.services.Model;

namespace chatapp.services.Implementation
{
    public class UserService : IUserService
    {

        public User FindUserByContextId(string contextId)
        {
            User user;
            var userGuid = Guid.Parse(contextId);

            using (var context = new DataDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.ContextId == userGuid);
            }

            return user;
        }

        public User FindUserByContextIdAndName(string name, string contextId)
        {
            User user;
            var userGuid = Guid.Parse(contextId);

            using (var context = new DataDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.ContextId == userGuid &&
                                                         x.ChatPseudo.Equals(name,
                                                             StringComparison.CurrentCultureIgnoreCase));
            }

            return user;
        }

        public User FindAgent(string name, Guid roomId)
        {
            User user;

            using (var context = new DataDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.ChatPseudo.Equals(name, StringComparison.CurrentCultureIgnoreCase)
                    && x.Type.Equals(UserType.One2OneAdmin.ToString())
                    && x.RoomId.Value.Equals(roomId));
            }

            return user;
        }

        public User CreateUser(string pseudo, string chatRoom, UserType type, Guid contextId, string chatRoomId="")
        {
            User user = null;

            using (var context = new DataDbContext())
            {
                ChatRoom room = null;
                user = new User
                {
                    ChatPseudo = pseudo,
                    DateCreated = DateTime.Now,
                    Email = "",
                    Type = type.ToString(),
                    ContextId = contextId
                };

                context.Users.Add(user);
                context.SaveChanges();

                if (!string.IsNullOrEmpty(chatRoomId))
                {
                    room = context.ChatRooms.FirstOrDefault(x => x.Id.Equals(new Guid(chatRoomId)));
                }
                else
                {
                    room = new ChatRoom
                    {
                        Name = chatRoom,
                        DateCreated = DateTime.Now,
                        CreatorId = user.Id
                    };
                }

                if (room != null)
                {
                    context.ChatRooms.Add(room);
                    context.SaveChanges();
                    user.RoomId = room.Id;
                    context.SaveChanges();
                }
            }
            //add cookies for userid and roomid
            
            return user;

        }

        public User CreateAgent(string pseudo, UserType type, Guid contextId, Guid roomId, Guid requestId)
        {
            User user = null;

            using (var context = new DataDbContext())
            {
                ChatRoom room = null;
                user = new User
                {
                    ChatPseudo = pseudo,
                    DateCreated = DateTime.Now,
                    Type = type.ToString(),
                    ContextId = contextId,
                    RoomId = roomId,
                    RespondingToRequestId = requestId
                };

                context.Users.Add(user);
                context.SaveChanges();}
            //add cookies for userid and roomid

            return user;

        }

        public User GetUser(string userid)
        {
            User user;

            using (var context = new DataDbContext())
            {
                var tempId = new Guid(userid);
                user = context.Users.FirstOrDefault(x => x.Id.Equals(tempId));
            }

            return user;
        }

        public User GetUser(Guid userid)
        {
            User user;

            using (var context = new DataDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.Id.Equals(userid));
            }

            return user;
        }

        public int CountUsersInChatRoom(Guid chatRoomId)
        {
            var count = 0;

            using (var context = new DataDbContext())
            {
                count = context.Users.Count(x => x.RoomId == chatRoomId);
            }

            return count;
        }
    }
}