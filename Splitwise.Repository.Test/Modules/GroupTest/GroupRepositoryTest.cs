using Moq;
using Xunit;
using System;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Models;
using System.Threading.Tasks;
using Splitwise.Repository.GroupRepository;
using System.Collections.Generic;
using System.Linq;

namespace Splitwise.Repository.Test.Modules.GroupTest
{
    public class GroupRepositoryTest
    {
        GroupRepository.GroupRepository gp = new GroupRepository.GroupRepository();
        public GroupRepositoryTest()
        {
        }
        [Fact]
        public void AddGroupTest()
        {
            Group groups = new Group()
            {
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false
            };
            AddGroup addGroup = new AddGroup()
            {
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false,
                Users = new List<GroupUserMapping>()
                {
                    new GroupUserMapping()
                    {
                        Email = "sp@gmail.com",
                        Name = "SP"
                    },
                    new GroupUserMapping()
                    {
                        Email = "ayaz@gmail.com",
                        Name = "Ayaz"
                    }
                }
            };
            var groupRepo = new Mock<IGroupRepository>();
            var list = new List<Group>();
            groupRepo.Setup(x => x.AddGroup(addGroup)).Returns(groups);
            var group = gp.AddGroup(addGroup);
            Assert.Equal(groups, group);
            //AddGroup addGroup = new AddGroup();
            //Assert.Throws<NotImplementedException>(() => gp.AddGroup(addGroup));
        }
        [Fact]
        public void EditGroupTest()
        {
            Group groups = new Group()
            {
                GroupId = 1,
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false
            };
            GroupDetails editGroup = new GroupDetails()
            {
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false,
                Users = new List<GroupUserMapping>()
                {
                    new GroupUserMapping()
                    {
                        Email = "sp@gmail.com",
                        Name = "SP"
                    },
                    new GroupUserMapping()
                    {
                        Email = "ayaz@gmail.com",
                        Name = "Ayaz"
                    }
                }
            };
            var groupRepo = new Mock<IGroupRepository>();
            groupRepo.Setup(x => x.EditGroup(1, editGroup)).Returns(groups);
            var group = gp.EditGroup(1,editGroup);
            Assert.Equal(groups, group);
            //AddGroup addGroup = new AddGroup();
            //Assert.ThrowsAsync<NotImplementedException>(() => gp.EditGroup(1,addGroup));
        }
        [Fact]
        public void DeleteGroupTest()
        {
            Group groups = new Group()
            {
                GroupId = 1,
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false
            };
            var groupRepo = new Mock<IGroupRepository>();
            var list = new List<Group>();
            groupRepo.Setup(x => x.DeleteGroup(1)); 
            var group = gp.DeleteGroup(1);
            Assert.Null(group);
            //Assert.ThrowsAsync<NotImplementedException>(() => gp.DeleteGroup(1));
        }
        [Fact]
        public void GetGroupTest()
        {
            List<Group> groups = new List<Group>()
            {
                new Group()
                {
                GroupId = 1,
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false
                },
            };
            var groupMembers = new List<GroupMember>()
            {
                new GroupMember()
                {
                    GroupMemberId = 1,
                    GroupId = 1,
                    UserId = "82467ddb-6ff7-46ee-80b0-3b306c63b430"
                },
            };

            var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            var groupRepo = new Mock<IGroupRepository>();
            groupRepo.Setup(x => x.GetGroup(userId)).Returns(groups);
            var group = gp.GetGroup(userId);
            Assert.Equal(groups, group);
            //var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            //Assert.Throws<NotImplementedException>(() => gp.GetGroup(userId));
        }
        [Fact]
        public void GetGroupByIdTestAsync()
        {
            var groupRepo = new Mock<IGroupRepository>();
            Group groups = new Group()
            {
                GroupId = 1,
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false
            };
            Group Expected = new Group()
            {
                GroupId = 1,
                Name = "Trip",
                CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
                Date = new DateTime(2020, 10, 25, 10, 19, 28),
                SimplyfyDebits = false,
                IsDeleted = false
            };
            
            groupRepo.Setup(x => x.GetGroupById(1)).Returns(groups);
            var group = gp.GetGroupById(1);
            Assert.Equal(groups, Expected);
        }
        [Fact]
        public void GetGroupExpenseListTest()
        {
            var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            var groupId = 1;
            Assert.ThrowsAsync<NotImplementedException>(() => gp.GetGroupExpenseList(groupId,userId));
        }
    }
}
