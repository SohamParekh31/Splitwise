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
        void AddGroupMember(AddGroup addGroup, string currentUserId, Group group);
        List<GroupReturn> GetGroup(string currentUserId);
        AddGroup GetGroupById(int id);
        void DeleteGroup(int id,string userId);
        void EditGroup(int id, AddGroup editGroup);
        List<Expense> GetGroupExpenseList(int id, string currentUserId);

    }
}
