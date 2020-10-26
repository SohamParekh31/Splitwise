using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.ActivityRepository
{
    public class ActivityRepository : IActivityRepository
    {
        private AppDbContext @object;

        public ActivityRepository()
        {

        }

        public ActivityRepository(AppDbContext @object)
        {
            this.@object = @object;
        }

        public List<Activity> ActivityList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
