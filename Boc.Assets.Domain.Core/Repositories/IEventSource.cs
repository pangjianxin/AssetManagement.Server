using Boc.Assets.Domain.Core.Events;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Repositories
{
    public interface IEventSource<TEvent> where TEvent : Event
    {
        IQueryable<TEvent> GetAll(Expression<Func<TEvent, bool>> predicate = null);
        Task<TEvent> GetByIdAsync(Guid id);
        Task<TEvent> SaveAsync(TEvent @event);
        Task<TEvent> RemoveAsync(TEvent @event);
        Task<TEvent> UpdateAsync(TEvent @event);
    }
}