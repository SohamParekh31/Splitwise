﻿using Splitwise.DomainModel.Models;
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
        Task DeleteFriend(string id,string userId);
        Task GetFriendsExpenseList(string friendId, string currentUserId);
    }
}
