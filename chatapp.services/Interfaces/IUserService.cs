using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chatapp.data.Model;
using chatapp.services.Model;

namespace chatapp.services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(string pseudo, string chatRoom, UserType type, Guid contextId, string chatRoomId = "");

        User GetUser(string userid);

        int CountUsersInChatRoom(Guid chatRoomId);

        User FindUserByContextIdAndName(string name, string contextId);

        User FindUserByContextId(string contextId);
    }
}
