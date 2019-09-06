using Boc.Assets.Domain.Core.Events;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Repositories
{
    public interface IEventStore
    {
        Task Save<T>(T @event) where T : Event;
    }
}