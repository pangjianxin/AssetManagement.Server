using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Core.SharedKernel;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository.EventSourcing
{
    public class EfCoreEventStore : IEventStore
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUser _user;

        public EfCoreEventStore(IEventRepository eventRepository, IUser user)
        {
            _eventRepository = eventRepository;
            _user = user;
        }
        public async Task Save<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new StoredEvent(
                @event,
                serializedData,
                _user.OrgIdentifier);

            await _eventRepository.StoreAsync(storedEvent);
        }
    }
}