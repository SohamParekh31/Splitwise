using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Repository.ActivityRepository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _appDbContext;

        public ActivityRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Activity> ActivityList(string id)
        {
            var activity = _appDbContext.Activities.Where(x => x.UserId == id).Include(e => e.group);
            return activity.ToList();
        }
    }
}
