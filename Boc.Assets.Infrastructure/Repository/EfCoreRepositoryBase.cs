using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, Guid> where TEntity : EntityBase
    {
        public EfCoreRepositoryBase(ApplicationDbContext context) : base(context)
        {
        }
    }
    public class EfCoreRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public EfCoreRepositoryBase(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
        /// <summary>
        /// synchronize add method
        /// </summary>
        /// <param name="obj"></param>
        public virtual TEntity Add(TEntity obj)
        {
            var entityEntry = DbSet.Add(obj);
            return entityEntry.Entity;
        }
        /// <summary>
        /// async add method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            var entityEntry = await DbSet.AddAsync(obj);
            return entityEntry.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> objects)
        {
            await DbSet.AddRangeAsync(objects);
        }

        /// <summary>
        /// dispose method inherit from IDisposable interface
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// GetAll method 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.AsNoTracking() : DbSet.Where(predicate).AsNoTracking();
        }
        /// <summary>
        /// async method GetAllList
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllListAsync()
        {
            return await DbSet.ToListAsync();
        }
        /// <summary>
        /// get entity by id,sync method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(TKey id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// get entity by id ,async method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }
        /// <summary>
        /// remove method
        /// </summary>
        /// <param name="id"></param>
        public virtual TEntity Remove(TKey id)
        {
            var entityEntry = DbSet.Remove(DbSet.Find(id));
            return entityEntry.Entity;
        }

        public TEntity Remove(TEntity obj)
        {
            DbSet.Remove(obj);
            return obj;
        }

        /// <summary>
        /// sync update method
        /// </summary>
        /// <param name="obj"></param>
        public virtual TEntity Update(TEntity obj)
        {
            var entityEntry = DbSet.Update(obj);
            return entityEntry.Entity;
        }
    }
}