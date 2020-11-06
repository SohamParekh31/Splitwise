using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityRepository(AppDbContext appDbContext,UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public List<Activity> ActivityList(string id)
        {
            var user = _userManager.FindByEmailAsync(id).Result;
            var activity = _appDbContext.Activities.Where(x => x.UserId == user.Id).Include(e => e.group);
            return activity.ToList();
        }
    }
}
