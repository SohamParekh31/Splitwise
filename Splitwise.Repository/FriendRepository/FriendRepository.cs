using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.AccountRepository;
using Splitwise.Repository.GroupRepository;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IGroupRepository _groupRepository;

        public FriendRepository(AppDbContext appDbContext,UserManager<ApplicationUser> userManager,
                    IAccountRepository accountRepository,IGroupRepository groupRepository)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _accountRepository = accountRepository;
            _groupRepository = groupRepository;
        }
        public Friend AddFriend(AddFriend addFriend)
        {
            var user = _userManager.FindByEmailAsync(addFriend.Friend2).Result;
            Friend friend = new Friend()
            {
                Friend1 = addFriend.Friend1,
                Friend2 = user.Id
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

        public List<Settlement> GetFriendsExpenseList(string friendId, string currentUserId)
        {
            var user = _userManager.FindByEmailAsync(currentUserId).Result;
            var friendExpenseYouOwe = _appDbContext.Settlements.Where(x => x.Payer == friendId && x.Payee == user.Id);
            var friendExpenseOwes = _appDbContext.Settlements.Where(x => x.Payer == user.Id && x.Payee == friendId);
            if (friendExpenseYouOwe != null)
                return friendExpenseYouOwe.ToList();
            else
                return friendExpenseOwes.ToList();
        }

        public List<Friend> GetFriendsList(string id)
        {
            var friend = _appDbContext.Friends.Where(x => x.Friend1 == id).Include(e => e.User2);
            return friend.ToList();
        }
        public UserModel GetFriendDetails(string email,string userId)
        {
            List<PayerModel> Owesfrom = new List<PayerModel>();
            List<PayerModel> Owsto = new List<PayerModel>();
            UserModel userModel = new UserModel();
            userModel.User = _userManager.FindByEmailAsync(email).Result;
            userModel.Groups = _groupRepository.GetGroup(email);
            var appUser = _userManager.FindByEmailAsync(email).Result;
            var settlements = _appDbContext.Settlements.Where(x => (x.Payee == appUser.Id && x.Payer == userId) || (x.Payee == userId && x.Payer == appUser.Id)).ToList();
            foreach (var item in settlements)
            { 
                if (item.Payee == appUser.Id)
                {
                    var user = _userManager.FindByIdAsync(item.Payer).Result;
                    PayerModel payerModel = new PayerModel()
                    {
                        Amount = item.Amount,
                        Payer = user,
                        PayerId = user.Id
                    };
                    Owesfrom.Add(payerModel);
                }
                else
                {
                    var user = _userManager.FindByIdAsync(item.Payee).Result;
                    PayerModel payerModel = new PayerModel()
                    {
                        Amount = item.Amount,
                        Payer = user,
                        PayerId = user.Id
                    };
                    Owsto.Add(payerModel);
                }
            }
            userModel.Owesfrom = Owesfrom;
            userModel.Owsto = Owsto;
            return userModel;
        }
    }
}
