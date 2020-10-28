using Moq;
using Xunit;
using System;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Models;
using System.Collections.Generic;
using System.Linq;
using Splitwise.Repository.GroupRepository;
using Castle.DynamicProxy;
using System.Linq.Expressions;
using Splitwise.Repository.DataRepository;
using System.Threading.Tasks;

namespace Splitwise.Repository.Test.Modules.GroupTest
{
    public class GroupRepositoryTest
    {
        //readonly GroupRepository.GroupRepository gp = new GroupRepository.GroupRepository();
        //public GroupRepositoryTest()
        //{
        //}
        //[Fact]
        //public void AddGroupTest()
        //{
        //    List<Group> groups = new List<Group>()
        //    {
                
        //    };
        //    AddGroup addGroup = new AddGroup()
        //    {
        //        Name = "Trip",
        //        CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //        Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //        SimplyfyDebits = false,
        //        IsDeleted = false,
        //        Users = new List<GroupUserMapping>()
        //        {
        //            new GroupUserMapping()
        //            {
        //                Email = "sp@gmail.com",
        //                Name = "SP"
        //            },
        //            new GroupUserMapping()
        //            {
        //                Email = "ayaz@gmail.com",
        //                Name = "Ayaz"
        //            }
        //        }
        //    };
        //    var mock = new Mock<IDataRepository>();
        //    mock.Setup(x => x.Where(It.IsAny<Expression<Func<Group, bool>>>())).Returns(groups.AsQueryable());
        //    var groupRepo = gp.AddGroup(addGroup);
        //    mock.Verify(x => x.Add<Group>(It.IsAny<Group>()), Times.Once());
        //    //AddGroup addGroup = new AddGroup();
        //    //Assert.Throws<NotImplementedException>(() => gp.AddGroup(addGroup));
        //}
        //[Fact]
        //public void EditGroupTest()
        //{
        //    List<Group> groups = new List<Group>()
        //    {
        //    };
            
        //    var mock = new Mock<IDataRepository>();
        //    mock.Setup(x => x.Where(It.IsAny<Expression<Func<Group, bool>>>())).Returns(groups.AsQueryable());
        //    mock.Verify(x => x.Update(It.IsAny<Group>()), Times.Once());
        //    //AddGroup addGroup = new AddGroup();
        //    //Assert.ThrowsAsync<NotImplementedException>(() => gp.EditGroup(1,addGroup));
        //}
        //[Fact]
        //public void DeleteGroupTest()
        //{
        //    List<Group> groups = new List<Group>()
        //    {
        //        new Group()
        //        {
        //            GroupId = 1,
        //            Name = "Trip",
        //            CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //            Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //            SimplyfyDebits = false,
        //            IsDeleted = false
        //        }
        //    };
        //    Group group = new Group()
        //    {
        //        GroupId = 1,
        //        Name = "Trip",
        //        CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //        Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //        SimplyfyDebits = false,
        //        IsDeleted = false
        //    };
        //    var mock = new Mock<IDataRepository>();;
        //    mock.Setup(x => x.Where(It.IsAny<Expression<Func<Group, bool>>>())).Returns(groups.AsQueryable());
        //    mock.Setup(x => x.Remove(group));
        //    gp.DeleteGroup(group.GroupId);
        //    mock.Verify(x => x.Remove(It.IsAny<Group>()), Times.Once());
        //}
        //[Fact]
        //public void GetGroupTest()
        //{
        //    var list = new List<Group>();
        //    var mock = new Mock<IDataRepository>();
        //    mock.Setup(x => x.Get<Group>()).Returns(Task.FromResult(list));

        //    var result = 0/*repo.ActivityList("1").Count()*/;
        //    var expected = 0;

        //    Assert.Equal(result, expected);
        //}
        //[Fact]
        //public void GetGroupByIdTestAsync()
        //{
        //    var groups = new List<Group>()
        //    {
        //        new Group()
        //        {
        //        GroupId = 1,
        //        Name = "Trip",
        //        CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //        Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //        SimplyfyDebits = false,
        //        IsDeleted = false
        //        },
        //    };

        //    var mock = new Mock<IDataRepository>();
        //    mock.Setup(x => x.Where<Group>(x => x.GroupId == 1)).Returns((IQueryable<Group>)groups);
        //    var group = gp.GetGroupById(1);
        //    Assert.Equal(group, group);
        //}
        //[Fact]
        //public void GetGroupExpenseListTest()
        //{
        //    var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
        //    var groupId = 1;
        //    Assert.ThrowsAsync<NotImplementedException>(() => gp.GetGroupExpenseList(groupId,userId));
        //}
    }
}
