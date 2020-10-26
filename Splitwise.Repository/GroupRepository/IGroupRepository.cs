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
        Group AddGroup(AddGroup addGroup);
        List<Group> GetGroup(string currentUserId);
        Group GetGroupById(int id);
        Task DeleteGroup(int id);
        Group EditGroup(int id, GroupDetails editGroup);
        Task GetGroupExpenseList(int id, string currentUserId);

    }
}
