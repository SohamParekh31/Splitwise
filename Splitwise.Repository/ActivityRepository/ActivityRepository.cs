using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.ActivityRepository
{
    public class ActivityRepository : IActivityRepository
    {
        private IDataRepository object1;

        public ActivityRepository()
        {

        }

        public ActivityRepository(IDataRepository object1)
        {
            this.object1 = object1;
        }

        public List<Activity> ActivityList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
