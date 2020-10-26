using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.GroupRepository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _appDbContext;

        public GroupRepository()
        {
        }

        public GroupRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Group AddGroup(AddGroup addGroup)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public Group EditGroup(int id, GroupDetails editGroup)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetGroup(string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Group GetGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetGroupExpenseList(int id, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
