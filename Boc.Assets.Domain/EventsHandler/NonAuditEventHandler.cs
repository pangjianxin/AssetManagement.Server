using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Notifications
{
    public class NonAuditEventHandler : INotificationHandler<NonAuditEvent>
    {
        public IEventSource<NonAuditEvent> _source;

        public NonAuditEventHandler(IEventSource<NonAuditEvent> source)
        {
            _source = source;
        }
        public async Task Handle(NonAuditEvent notification, CancellationToken cancellationToken)
        {
            await _source.SaveAsync(notification);
        }
    }
}