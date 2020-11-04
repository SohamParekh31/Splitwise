using Moq;
using Xunit;
using System;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Models;
using System.Collections.Generic;
using System.Linq;
using Splitwise.Repository.ActivityRepository;
using Splitwise.Repository.DataRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Splitwise.Repository.Test.Modules.ActivityTest
{
    public class ActivityRepositoryTest
    {
        public ActivityRepositoryTest()
        {
 
        }
        //[Fact]
        //public void ActivityListTest()
        //{
        //    List<Activity> list = new List<Activity>();
        //    var mock = new Mock<IDataRepository>();
        //    mock.Setup(x => x.Get<Activity>()).Returns(Task.FromResult(list));

        //    var repo = new ActivityRepository.ActivityRepository(mock.Object);
        //    var result = 0/*repo.ActivityList("1").Count()*/;
        //    var expected = 0;

        //    Assert.Equal(result, expected);


        //    //var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
        //    //Assert.Throws<NotImplementedException>(() => ap.ActivityList(userId));
        //}
    }
}
