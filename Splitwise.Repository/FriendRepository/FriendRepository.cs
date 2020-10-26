using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.FriendRepository
{
    public class FriendRepository : IFriendRepository
    {
        public Friend AddFriend(AddFriend addFriend)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFriend(string id,string userId)
        {
            throw new NotImplementedException();
        }

        public Task GetFriendsExpenseList(string friendId, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public List<Friend> GetFriendsList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
