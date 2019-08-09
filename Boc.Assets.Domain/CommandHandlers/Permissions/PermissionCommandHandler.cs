using Boc.Assets.Domain.Commands.Permissions;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Permissions
{
    public class PermissionCommandHandler : CommandHandler,
        IRequestHandler<ModifyPermissionCommand, bool>
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IPermissionRepository permissionRepository) : base(unitOfWork, bus, notifications)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<bool> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            //首先将对应的permission都删除
            await _permissionRepository.RemoveRangeByRoleId(request.RoleId);
            List<Permission> permissions = new List<Permission>();
            foreach (var item in request.Permissions)
            {
                permissions.Add(new Permission()
                {
                    ActionName = item.Action,
                    ControllerName = item.Controller,
                    RoleId = request.RoleId
                });
            }
            await _permissionRepository.AddRangeAsync(permissions);
            if (await CommitAsync())
            {
                return true;
            }
            return false;
        }
    }
}