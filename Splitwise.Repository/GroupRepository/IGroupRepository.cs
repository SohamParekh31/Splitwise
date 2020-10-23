using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.GroupRepository
{
    public interface IGroupRepository
    {
        Task<Group> AddGroup(AddGroup addGroup);
        Task GetGroup(string currentUserId);
        Task GetGroupById(int id);
        Task DeleteGroup(int id);
        Task<Group> EditGroup(int id, AddGroup editGroup);
        Task GetGroupExpenseList(int id, string currentUserId);

    }
}
