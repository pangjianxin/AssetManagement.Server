using Boc.Assets.Domain.Authentication;
using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Organization;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Organization
{
    public class OrganizationCommandHandler : CommandHandler,
        IRequestHandler<ChangeOrgShortNameCommand, string>,
        IRequestHandler<ChangeOrgPasswordCommand, string>,
        IRequestHandler<ResetOrgPasswordCommand, bool>,
        IRequestHandler<LoginCommand, string>
    {
        private readonly IOrganizationRepository _orgRepository;
        private readonly IUser _user;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtFactory _jwtFactory;

        public OrganizationCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrganizationRepository orgRepository,
            IUser user,
            IPasswordHasher passwordHasher,
            IJwtFactory jwtFactory) : base(unitOfWork, bus, notifications)
        {
            _orgRepository = orgRepository;
            _user = user;
            _passwordHasher = passwordHasher;
            _jwtFactory = jwtFactory;
        }


        public async Task<string> Handle(ChangeOrgShortNameCommand command, CancellationToken cancellationToken)
        {
            //验证参数是否通过验证，如果没有，则写入DomainNotification中。
            if (!command.IsValid())
            {
                await NotifyValidationErrors(command);
                return string.Empty;
            }
            var org = await _orgRepository.GetByOrgIdentifierAsync(command.OrgIdentifier);
            var beforeModifiedShortName = org.OrgShortNam;
            var afterModifiedShortName = org.ChangeOrgShortName(command.OrgShortNam);
            _orgRepository.Update(org);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrgShortNameChangedEvent(_user.OrgId, beforeModifiedShortName, afterModifiedShortName));
                return await _jwtFactory.CreateTokenAsync(org);
            }
            return string.Empty;
        }

        public async Task<string> Handle(ChangeOrgPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return string.Empty;
            }

            if (request.NewPassword.Length < 6)
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端", "输入的密码位数不够，请重新输入"));
                return string.Empty;
            }
            var org = await _orgRepository.GetByOrgIdentifierAsync(request.OrgIdentifier);
            if (org == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端", "未找到机构"));
                return string.Empty;
            }
            var testHash = _passwordHasher.Hash(request.OldPassword, org.Salt);
            if (!org.Hash.SequenceEqual(testHash))
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端", "旧密码不对"));
                return string.Empty;
            }

            if (!request.NewPassword.Equals(request.ConfirmPassword))
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端", "前后输入的新密码不一致"));
                return string.Empty;
            }
            var newSalt = Guid.NewGuid().ToByteArray();
            var newHash = _passwordHasher.Hash(request.NewPassword, newSalt);
            org.Hash = newHash;
            org.Salt = newSalt;
            _orgRepository.Update(org);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrgPasswordChangedEvent(_user.OrgId, org.OrgNam, org.OrgIdentifier));
                return await _jwtFactory.CreateTokenAsync(org);
            }
            return string.Empty;
        }

        public async Task<bool> Handle(ResetOrgPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var org = await _orgRepository.GetByOrgIdentifierAsync(request.OrgIdentifier);
            //新作一个盐
            var salt = Guid.NewGuid().ToByteArray();
            //重置密码采用6个0
            var hash = _passwordHasher.Hash("000000", salt);
            org.Hash = hash;
            org.Salt = salt;
            _orgRepository.Update(org);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrgPasswordResetEvent(_user.OrgId, org.OrgNam, org.OrgIdentifier));
                return true;
            }
            return false;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return string.Empty;
            }

            var org = await _orgRepository.GetByOrgIdentifierAsync(request.OrgIdentifier);
            if (org == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端", "用户名不存在"));
                return string.Empty;
            }

            if (!org.Hash.SequenceEqual(_passwordHasher.Hash(request.Password, org.Salt)))
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端", "密码错误"));
                return string.Empty;
            }
            var token = await _jwtFactory.CreateTokenAsync(org);
            return token;
        }
    }
}