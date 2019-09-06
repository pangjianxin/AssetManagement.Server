using Boc.Assets.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Repositories
{
    public interface IEventRepository : IDisposable
    {
        Task StoreAsync(StoredEvent theEvent);
        Task<IList<StoredEvent>> AllAsync(Guid aggregateId);
    }
}