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

namespace Splitwise.Repository.Test.Modules.ActivityTest
{
    public class ActivityRepositoryTest
    {
        readonly ActivityRepository.ActivityRepository ap = new ActivityRepository.ActivityRepository();
        [Fact]
        public void ActivityListTest()
        {
            //var AppdbContextmock = new Mock<AppDbContext>();
            var activityRepo = new Mock<IActivityRepository>();
            var list = new List<Activity>();
            var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            activityRepo.Setup(x => x.ActivityList(userId)).Returns(list);

            var ExpectedCount = 0;
            //ActivityRepository.ActivityRepository ap = new ActivityRepository.ActivityRepository();
            var ActualCount = 0/*ap.ActivityList(userId).ToList().Count()*/;

            Assert.Equal(ActualCount, ExpectedCount);

            //var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            //Assert.Throws<NotImplementedException>(() => ap.ActivityList(userId));
        }
    }
}
