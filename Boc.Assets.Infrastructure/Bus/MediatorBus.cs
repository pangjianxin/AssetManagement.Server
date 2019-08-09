using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Bus
{

    public class MediatorBus : IBus
    {
        private readonly IMediator _mediator;

        public MediatorBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task RaiseEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            await _mediator.Publish(@event);
        }

        public async Task<bool> SendCommandAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}