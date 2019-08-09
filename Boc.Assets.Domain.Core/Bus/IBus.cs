using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.Events;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Bus
{
    public interface IBus
    {
        Task<bool> SendCommandAsync<TCommand>(TCommand command) where TCommand : Command;
        Task RaiseEventAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}