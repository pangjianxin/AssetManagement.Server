using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Organization;
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
        private readonly IUser _user;

        public OrganizationCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrganizationRepository orgRepository,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _orgRepository = orgRepository;
            _user = user;
        }


        public async Task<bool> Handle(ChangeOrgShortNameCommand command, CancellationToken cancellationToken)
        {
            //验证参数是否通过验证，如果没有，则写入DomainNotification中。
            if (!command.IsValid())
            {
                await NotifyValidationErrors(command);
                return false;
            }
            var org = await _orgRepository.GetByOrgIdentifierAsync(command.OrgIdentifier);
            var beforeModifiedShortName = org.OrgShortNam;
            var afterModifiedShortName = org.ChangeOrgShortName(command.OrgShortNam);
            _orgRepository.Update(org);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrgShortNameChangedEvent(_user.OrgId, beforeModifiedShortName, afterModifiedShortName));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ChangeOrgPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var org = await _orgRepository.GetByOrgIdentifierAsync(request.OrgIdentifier);
            org.ChangeOrgPassword(request.NewPassword);
            _orgRepository.Update(org);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrgPasswordChangedEvent(_user.OrgId, org.OrgNam, org.OrgIdentifier));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(ResetOrgPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var org = await _orgRepository.GetByOrgIdentifierAsync(request.OrgIdentifier);
            org.ResetPassword();
            _orgRepository.Update(org);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrgPasswordResetEvent(_user.OrgId, org.OrgNam, org.OrgIdentifier));
                return false;
            }
            return true;
        }
    }
}