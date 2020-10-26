using Moq;
using Xunit;
using System;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.FriendRepository;
using Splitwise.DomainModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Splitwise.Repository.Test.Modules.FriendTest
{
    public class FriendRepositoryTest
    {
        FriendRepository.FriendRepository fp = new FriendRepository.FriendRepository();
        [Fact]
        public void AddFriendTest()
        {
            Friend friends = new Friend()
            {
                Friend1 = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Friend2 = "82467ddb-6ff7-46ee-80b0-3b306c63b430"
            };
            AddFriend addFriend = new AddFriend()
            {
                Friend1 = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Friend2 = "82467ddb-6ff7-46ee-80b0-3b306c63b430"
            };
            var friendRepo = new Mock<IFriendRepository>();
            friendRepo.Setup(x => x.AddFriend(addFriend)).Returns(friends);
            var friend = fp.AddFriend(addFriend);
            Assert.Equal(friends, friend);
            //AddFriend addFriend = new AddFriend();
            //Assert.ThrowsAsync<NotImplementedException>(() => fp.AddFriend(addFriend));
        }
        [Fact]
        public void DeleteFriendTest()
        {
            Friend friends = new Friend()
            {
                Friend1 = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Friend2 = "82467ddb-6ff7-46ee-80b0-3b306c63b430"
            };
            var friendRepo = new Mock<IFriendRepository>();
            var list = new List<Friend>();
            var userId = "82467ddb - 6ff7 - 46ee - 80b0 - 3b306c63b430";
            friendRepo.Setup(x => x.DeleteFriend(userId,userId));
            var group = fp.DeleteFriend(userId,userId);
            Assert.Null(group);
        }
        [Fact]
        public void GetFriendsExpenseListTest()
        {
            var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            var friendId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            Assert.ThrowsAsync<NotImplementedException>(() => fp.GetFriendsExpenseList(friendId,userId));
        }
        [Fact]
        public void GetFriendsListTest()
        {
            List<Friend> friends = new List<Friend>()
            {
                new Friend()
                {
                    Friend1 = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                    Friend2 = "82467ddb-6ff7-46ee-80b0-3b306c63b430"
                }
            };
            var friendRepo = new Mock<IFriendRepository>();
            var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            friendRepo.Setup(x => x.GetFriendsList(userId)).Returns(friends);
            var group = fp.GetFriendsList(userId).ToList().Count;
            Assert.Equal(friends.Count,group);
        }
    }
}
