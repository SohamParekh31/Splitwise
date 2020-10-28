using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Splitwise.DomainModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.DataRepository
{
    public class DataRepository : IDataRepository
    {
        private readonly AppDbContext appDbContext;

        public DataRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public DbSet<T> SetDb<T>() where T : class
        {
            DbSet<T> table = appDbContext.Set<T>();
            return table;
        }
        private DbSet<T> CreateDbSet<T>() where T : class
        {
            return appDbContext.Set<T>();
        }
        public EntityEntry<T> Add<T>(T entity) where T : class
        {
            var dbSet = CreateDbSet<T>();
            return dbSet.Add(entity);
        }
        public EntityEntry<T> Update<T>(T entity) where T : class
        {
            var dbSet = CreateDbSet<T>();
            return dbSet.Update(entity);
        }

        public async Task AddAsync<T>(T obj) where T : class
        {
            DbSet<T> table = SetDb<T>();
            await table.AddAsync(obj);
        }

        public async Task AddRangeAsync<T>(List<T> obj) where T : class
        {
            DbSet<T> table = SetDb<T>();
            await table.AddRangeAsync(obj);
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class
        {
            DbSet<T> table = SetDb<T>();
            return table.Where(expression);
        }

        public void Remove<T>(T obj) where T : class
        {
            DbSet<T> table = SetDb<T>();
            table.Remove(obj);
        }

        public void RemoveRange<T>(List<T> obj) where T : class
        {
            DbSet<T> table = SetDb<T>();
            table.RemoveRange(obj);
        }

        public async Task<List<T>> Get<T>() where T : class
        {
            DbSet<T> table = SetDb<T>();
            return await table.ToListAsync();
        }

    }
}
