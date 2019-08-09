using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Organization
{
    public class OrganizationCommandHandler : CommandHandler,
        IRequestHandler<ChangeOrgShortNameCommand, bool>,
        IRequestHandler<ChangeOrgPasswordCommand, bool>,
        IRequestHandler<ResetOrgPasswordCommand, bool>
    {
        private readonly IOrganizationRepository _orgRepository;

        public OrganizationCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrganizationRepository orgRepository) : base(unitOfWork, bus, notifications)
        {
            _orgRepository = orgRepository;
        }


        public async Task<bool> Handle(ChangeOrgShortNameCommand command, CancellationToken cancellationToken)
        {
            //验证参数是否通过验证，如果没有，则写入DomainNotification中。
            if (!await command.IsValid())
            {
                await NotifyValidationErrors(command);
                return false;
            }
            var org = await _orgRepository.ChangeOrgShortNameAsync(command.OrgIdentifier, command.OrgShortNam);
            if (await CommitAsync())
            {
                await _bus.RaiseEventAsync(
                    new NonAuditEvent(command.Principal, NonAuditEventType.机构简称变更));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ChangeOrgPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var org =
                await _orgRepository.ChangeOrgPassword(request.OrgIdentifier, request.OldPassword, request.NewPassword);
            if (await CommitAsync())
            {
                await _bus.RaiseEventAsync(
                    new NonAuditEvent(request.Principal, NonAuditEventType.机构密码变更));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ResetOrgPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var org = await _orgRepository.ResetOrgPassword(request.OrgIdentifier);
            if (await CommitAsync())
            {
                await _bus.RaiseEventAsync(
                    new NonAuditEvent(request.Principal, NonAuditEventType.机构密码重置));
                return false;
            }
            return true;
        }
    }
}