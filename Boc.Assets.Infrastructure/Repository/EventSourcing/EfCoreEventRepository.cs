using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository.EventSourcing
{
    public class EfCoreEventRepository : IEventRepository
    {
        private readonly EventStoreDbContext _context;

        public EfCoreEventRepository(EventStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IList<StoredEvent>> AllAsync(Guid aggregateId)
        {
            return await (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToListAsync();
        }

        public async Task StoreAsync(StoredEvent theEvent)
        {
            await _context.StoredEvents.AddAsync(theEvent);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}