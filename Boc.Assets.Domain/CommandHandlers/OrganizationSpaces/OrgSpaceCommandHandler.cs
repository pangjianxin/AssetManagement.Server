using Boc.Assets.Domain.Commands.OrganizationSpace;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.OrganizationSpaces
{
    public class OrgSpaceCommandHandler : CommandHandler,
        IRequestHandler<CreateSpaceCommand, bool>,
        IRequestHandler<ModifySpaceInfoCommand, bool>
    {
        private readonly IOrgSpaceRepository _orgSpaceRepository;

        public OrgSpaceCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrgSpaceRepository orgSpaceRepository) : base(unitOfWork, bus, notifications)
        {
            _orgSpaceRepository = orgSpaceRepository;
        }

        public async Task<bool> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
        {
            if (! request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var space = await _orgSpaceRepository.CreateSpaceAsync(request.SpaceName,
                request.SpaceDescription,
                request.Principal.OrgId,
                request.Principal.OrgIdentifier,
                request.Principal.OrgNam);
            if (await CommitAsync())
            {
                await _bus.RaiseEventAsync(new NonAuditEvent(request.Principal, NonAuditEventType.新增机构空间));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ModifySpaceInfoCommand request, CancellationToken cancellationToken)
        {
            if (! request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var space = await _orgSpaceRepository.ModifySpaceAsync(request.SpaceId, request.SpaceName,
                request.SpaceDescription);
            if (await CommitAsync())
            {
                await _bus.RaiseEventAsync(new NonAuditEvent(request.Principal, NonAuditEventType.机构空间名称或描述变更));
                return true;
            }
            return false;
        }
    }
}