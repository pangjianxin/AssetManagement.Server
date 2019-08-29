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
        private readonly IUser _user;

        public OrgSpaceCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrgSpaceRepository orgSpaceRepository,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _orgSpaceRepository = orgSpaceRepository;
            _user = user;
        }

        public async Task<bool> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var space = await _orgSpaceRepository.CreateSpaceAsync(request.SpaceName,
                request.SpaceDescription,
                _user.OrgId,
                _user.OrgIdentifier,
                _user.OrgNam);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new NonAuditEvent(_user, NonAuditEventType.新增机构空间));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ModifySpaceInfoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var space = await _orgSpaceRepository.ModifySpaceAsync(request.SpaceId, request.SpaceName,
                request.SpaceDescription);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new NonAuditEvent(_user, NonAuditEventType.机构空间名称或描述变更));
                return true;
            }
            return false;
        }
    }
}