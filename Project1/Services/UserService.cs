
using System;
using System.Linq;
using chatapp.data.Model;
using Project1.Models;

namespace Services
{
    public class UserService
    {
        public User CreateUser(string pseudo, string chatRoom, UserType type, string chatRoomId="")
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
                    Type = type.ToString()
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

            return user;

        }


    }
}