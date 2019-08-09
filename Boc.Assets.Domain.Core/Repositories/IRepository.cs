using Boc.Assets.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>, IDisposable where TEntity : EntityBase
    {

    }
    public interface IRepository<TEntity, in TKey> : IDisposable where TEntity : EntityBase<TKey>
    {
        #region 同步接口
        TEntity Add(TEntity obj);
        TEntity GetById(TKey id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        TEntity Update(TEntity obj);
        TEntity Remove(TKey id);
        TEntity Remove(TEntity obj);
        #endregion
        #region 异步接口

        Task<TEntity> AddAsync(TEntity obj);
        Task AddRangeAsync(IEnumerable<TEntity> objects);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllListAsync();

        #endregion
    }
}