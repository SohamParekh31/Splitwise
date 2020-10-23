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
        public Task AddFriend(AddFriend addFriend)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFriend(string id)
        {
            throw new NotImplementedException();
        }

        public Task GetFriendsExpenseList(string friendId, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Friend>> GetFriendsList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
