using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Bus
{

    public class MediatorBus : IBus
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public MediatorBus(IMediator mediator,
            IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }
        public async Task RaiseEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            if (!@event.MessageType.Equals(nameof(DomainNotification)))
            {
                await _eventStore.Save(@event);
            }
            await _mediator.Publish(@event);
        }

        public async Task<TResult> SendCommandAsync<TResult>(Command<TResult> command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}