using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.DataRepository
{
    public interface IDataRepository
    {
        EntityEntry<T> Add<T>(T entity) where T : class;
        EntityEntry<T> Update<T>(T entity) where T : class;
        Task AddAsync<T>(T obj) where T : class;
        Task AddRangeAsync<T>(List<T> obj) where T : class;
        IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class;
        void Remove<T>(T obj) where T : class;
        void RemoveRange<T>(List<T> obj) where T : class;
        Task<List<T>> Get<T>() where T : class;
    }
}
