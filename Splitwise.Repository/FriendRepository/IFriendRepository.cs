using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.FriendRepository
{
    public interface IFriendRepository
    {
        Friend AddFriend(AddFriend addFriend);
        List<Friend> GetFriendsList(string id);
        void DeleteFriend(string id,string userId);
        List<Settlement> GetFriendsExpenseList(string friendId, string currentUserId);
        UserModel GetFriendDetails(string email);
    }
}
