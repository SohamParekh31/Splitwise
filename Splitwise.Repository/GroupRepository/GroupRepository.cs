﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.GroupRepository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public Group AddGroup(AddGroup addGroup)
        {
            Group group = new Group()
            {
                Name = addGroup.Name,
                CreatedBy = addGroup.CreatedBy,
                Date = addGroup.Date,
                SimplyfyDebits = addGroup.SimplyfyDebits,
                IsDeleted = false
            };
            _appDbContext.Groups.Add(group);
            _appDbContext.SaveChanges();

            return group;
        }

        public void AddGroupMember(AddGroup addGroup, string email, Group group)
        {
            var checkCurrentUser = _userManager.FindByEmailAsync(email).Result;
            GroupMember groupMember1 = new GroupMember()
            {
                GroupId = group.GroupId,
                UserId = addGroup.CreatedBy,
            };
            Activity activity = new Activity()
            {
                Description = "You Created the Group " + group.Name,
                UserId = checkCurrentUser.Id,
                Date = addGroup.Date,
            };
            _appDbContext.Activities.Add(activity);
            _appDbContext.GroupMembers.Add(groupMember1);
            _appDbContext.SaveChanges();
            foreach (var item in addGroup.Users)
            {
                var user = _userManager.FindByEmailAsync(item.Email).Result;
                var checkFriend = _appDbContext.Friends.Where(x => x.Friend1 == addGroup.CreatedBy && x.Friend2 == user.Id);
                if (checkFriend == null)
                {
                    Friend friend = new Friend()
                    {
                        Friend1 = addGroup.CreatedBy,
                        Friend2 = user.Id
                    };
                    _appDbContext.Friends.Add(friend);
                    _appDbContext.SaveChanges();
                }
                GroupMember groupMember = new GroupMember()
                {
                    GroupId = group.GroupId,
                    UserId = user.Id
                };
                Activity activity1 = new Activity()
                {
                    Description = "You Were Added to the Group " + group.Name,
                    UserId = user.Id,
                    Date = addGroup.Date,
                };
                _appDbContext.Activities.Add(activity1);
                _appDbContext.GroupMembers.Add(groupMember);
                _appDbContext.SaveChanges();
            }
        }

        public void DeleteGroup(int id, string userId)
        {
            var user = _userManager.FindByEmailAsync(userId).Result;
            var groupMemberDelete = _appDbContext.GroupMembers.Where(x => x.GroupId == id);
            _appDbContext.GroupMembers.RemoveRange(groupMemberDelete);
            var expense = _appDbContext.Expenses.Where(x => x.GroupId == id).ToList();
            if(expense.Count != 0)
                _appDbContext.Expenses.RemoveRange(expense);
            var getExpenseId = _appDbContext.Expenses.FirstOrDefault(x => x.GroupId == id);
            if(getExpenseId != null)
            {
                var expenseInfo = _appDbContext.ExpenseInfos.Where(x => x.ExpenseId == getExpenseId.ExpenseId).ToList();
                _appDbContext.ExpenseInfos.RemoveRange(expenseInfo);
            }
            var settlements = _appDbContext.Settlements.Where(x => x.GroupId == id);
            _appDbContext.Settlements.RemoveRange(settlements);
            var group = _appDbContext.Groups.FirstOrDefault(x => x.GroupId == id);
            _appDbContext.Groups.Remove(group);
            Activity activity1 = new Activity()
            {
                Description = "You Deleted the Group " + group.Name,
                UserId = user.Id,
                Date = DateTime.Today
            };
            _appDbContext.Activities.Add(activity1);
            _appDbContext.SaveChanges();
        }

        public void EditGroup(int id, AddGroup editGroup)
        {
            var group = _appDbContext.Groups.ToList().FirstOrDefault(x => x.GroupId == editGroup.GroupId);
            var groupMember = _appDbContext.GroupMembers.Where(x => x.GroupId == group.GroupId);
            _appDbContext.GroupMembers.RemoveRange(groupMember);
            group.GroupId = editGroup.GroupId;
            group.Name = editGroup.Name;
            group.Date = editGroup.Date;
            group.SimplyfyDebits = editGroup.SimplyfyDebits;
            group.IsDeleted = editGroup.IsDeleted;
            _appDbContext.Groups.Update(group);
            GroupMember member = new GroupMember() 
            {
                GroupId = group.GroupId,
                UserId = group.CreatedBy
            };
            _appDbContext.GroupMembers.Add(member);
            _appDbContext.SaveChanges();
            foreach (var item in editGroup.Users.ToList())
            {
                GroupMember groupMembers = new GroupMember();
                groupMembers.GroupId = group.GroupId;
                var user = _userManager.FindByEmailAsync(item.Email).Result;
                groupMembers.UserId = user.Id;

                _appDbContext.GroupMembers.Add(groupMembers);
                _appDbContext.SaveChanges();
            }
        }

        public List<GroupReturn> GetGroup(string currentUserId)
        {
            var user = _userManager.FindByEmailAsync(currentUserId).Result;
            var group = _appDbContext.Groups.ToList();
            var groupMember = _appDbContext.GroupMembers.ToList();
            List<GroupReturn> groupReturns = new List<GroupReturn>();
            var groupList = groupMember.Where(x => x.UserId == user.Id).Join(group, x => x.GroupId, y => y.GroupId, (x, group) => new
            {
                GroupName = group.Name,
                GroupId = group.GroupId,
                UserId = x.UserId,
                createdBy = group.CreatedBy
            });
            foreach (var item in groupList)
            {
                GroupReturn groupReturn = new GroupReturn()
                {
                    GroupId = item.GroupId,
                    Name = item.GroupName,
                    UserId = item.UserId,
                    createdBy = item.createdBy
                };
                groupReturns.Add(groupReturn);
            }
            return groupReturns;
        }

        public AddGroup GetGroupById(int id)
        {
            var groupList = _appDbContext.Groups.FirstOrDefault(x => x.GroupId == id);
            var groupMemberList = _appDbContext.GroupMembers.Where(x => x.GroupId == id).ToList();
            AddGroup addGroup = new AddGroup();
            addGroup.GroupId = groupList.GroupId;
            addGroup.Name = groupList.Name;
            addGroup.CreatedBy = groupList.CreatedBy;
            var user = _userManager.FindByIdAsync(addGroup.CreatedBy).Result;
            addGroup.User = user;
            addGroup.Date = groupList.Date;
            addGroup.SimplyfyDebits = groupList.SimplyfyDebits;
            addGroup.IsDeleted = groupList.IsDeleted;
            addGroup.Users = new List<GroupUserMapping>();
            foreach (var item in groupMemberList)
            {
                if(item.UserId != addGroup.CreatedBy)
                {
                    var applicationUser = _userManager.FindByIdAsync(item.UserId).Result;
                    GroupUserMapping groupUserMapping = new GroupUserMapping
                    {
                        Name = applicationUser.FirstName,
                        Email = applicationUser.Email
                    };
                    addGroup.Users.Add(groupUserMapping);
                }
            }
            
            return addGroup;
        }

        public List<Expense> GetGroupExpenseList(int id, string currentUserId)
        {
            var groupExpense = _appDbContext.Expenses.Where(x => x.GroupId == id).Include(e => e.User).Include(e => e.Group);
            return groupExpense.ToList();
        }
    }
}
