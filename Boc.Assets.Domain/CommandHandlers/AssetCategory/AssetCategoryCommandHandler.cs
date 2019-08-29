﻿using Boc.Assets.Domain.Commands.AssetCategory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.AssetCategory
{
    public class AssetCategoryCommandHandler : CommandHandler,
        IRequestHandler<ChangeMeteringUnitCommand, bool>
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IOrganizationRepository _orgRepository;
        private readonly IUser _user;

        public AssetCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IAssetCategoryRepository assetCategoryRepository,
            IOrganizationRepository orgRepository,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetCategoryRepository = assetCategoryRepository;
            _orgRepository = orgRepository;
            _user = user;
        }

        public async Task<bool> Handle(ChangeMeteringUnitCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            await _assetCategoryRepository.ChangeMeteringUnitAsync(request.AssetCategoryId, request.AssetMeteringUnit);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new NonAuditEvent(_user, NonAuditEventType.资产分类计量单位变更));
                return true;
            }
            return false;
        }
    }
}