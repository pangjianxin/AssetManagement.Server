using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository.EventSourcing
{
    public class EfCoreEventSource<TEvent> : IEventSource<TEvent>
        where TEvent : Event
    {
        protected readonly IUser _user;
        protected readonly EventStoreDbContext _context;
        private DbSet<TEvent> Events => _context.Set<TEvent>();
        public EfCoreEventSource(EventStoreDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<TEvent> SaveAsync(TEvent @event)
        {
            await Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task<TEvent> RemoveAsync(TEvent @event)
        {
            Events.Remove(@event);
            await _context.SaveChangesAsync();
            return @event;
        }

        public IQueryable<TEvent> GetAll(Expression<Func<TEvent, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return Events.AsNoTracking();
            }
            return Events.Where(predicate).AsNoTracking();
        }

        public async Task<TEvent> GetByIdAsync(Guid id)
        {
            return await Events.FindAsync(id);
        }

        public async Task<TEvent> UpdateAsync(TEvent @event)
        {
            Events.Update(@event);
            await _context.SaveChangesAsync();
            return @event;
        }
    }
}