using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.ActivityRepository
{
    public interface IActivityRepository
    {
        public List<Activity> ActivityList(string id);
    }
}
