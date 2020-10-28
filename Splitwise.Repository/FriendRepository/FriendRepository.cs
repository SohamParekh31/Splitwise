using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.FriendRepository
{
    public class FriendRepository : IFriendRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public FriendRepository(AppDbContext appDbContext,UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public Friend AddFriend(AddFriend addFriend)
        {
            Friend friend = new Friend()
            {
                Friend1 = addFriend.Friend1,
                Friend2 = addFriend.Friend2
            };
            _appDbContext.Friends.Add(friend);
            _appDbContext.SaveChanges();
            return friend;
        }

        public void DeleteFriend(string id,string userId)
        {
            var user = _userManager.FindByEmailAsync(userId).Result;
            var friend = _appDbContext.Friends.FirstOrDefault(x => x.Friend2 == id && x.Friend1 == user.Id);
            _appDbContext.Friends.Remove(friend);
            _appDbContext.SaveChanges();
        }

        public Task GetFriendsExpenseList(string friendId, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public List<Friend> GetFriendsList(string id)
        {
            var friend = _appDbContext.Friends.Where(x => x.Friend1 == id).Include(e => e.User2);
            return friend.ToList();
        }
    }
}
