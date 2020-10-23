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
        public Task<Group> AddGroup(AddGroup addGroup)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> EditGroup(int id, AddGroup editGroup)
        {
            throw new NotImplementedException();
        }

        public Task GetGroup(string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task GetGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetGroupExpenseList(int id, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
