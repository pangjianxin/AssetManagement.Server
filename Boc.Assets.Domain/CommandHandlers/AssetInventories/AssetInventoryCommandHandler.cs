using Boc.Assets.Domain.Commands.AssetInventory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.AssetInventories
{
    public class AssetInventoryCommandHandler : CommandHandler,
        IRequestHandler<CreateAssetInventoryCommand, bool>,
        IRequestHandler<CreateAssetInventoryDetailCommand, bool>
    {
        private readonly IAssetInventoryRepository _assetInventoryRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetInventoryRegisterRepository _assetInventoryRegisterRepository;
        private readonly IAssetInventoryDetailRepository _assetInventoryDetailRepository;
        private readonly IUser _user;

        public AssetInventoryCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IAssetInventoryRepository assetInventoryRepository,
            IOrganizationRepository organizationRepository,
            IAssetInventoryRegisterRepository assetInventoryRegisterRepository,
            IAssetInventoryDetailRepository assetInventoryDetailRepository,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetInventoryRepository = assetInventoryRepository;
            _organizationRepository = organizationRepository;
            _assetInventoryRegisterRepository = assetInventoryRegisterRepository;
            _assetInventoryDetailRepository = assetInventoryDetailRepository;
            _user = user;
        }

        public async Task<bool> Handle(CreateAssetInventoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var publisher = await _organizationRepository.GetByIdAsync(_user.OrgId);
            if (publisher == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "PublisherId没有找到相应的数据"));
                return false;
            }

            var assetStockTaking = new AssetInventory(publisher, request.TaskName, request.TaskComment, request.ExpiryDateTime);
            IEnumerable<Models.Organizations.Organization> targetOrganizations;
            if (request.ExcludedOrganizations != null && request.ExcludedOrganizations.Any())
            {
                targetOrganizations = _organizationRepository.GetAll(it => it.OrgLvl == "4"
                                                                           && it.Org2 == publisher.Org2
                                                                           && !request.ExcludedOrganizations.Contains(
                                                                               it.Id));
            }
            else
            {
                targetOrganizations =
                    _organizationRepository.GetAll(it => it.OrgLvl == "4" && it.Org2 == publisher.Org2);
            }

            var assetStockTakingOrganizations = new List<AssetInventoryRegister>();
            foreach (var targetOrganization in targetOrganizations)
            {
                assetStockTakingOrganizations.Add(new AssetInventoryRegister(targetOrganization.Id, assetStockTaking.Id));
            }

            await _assetInventoryRepository.AddAsync(assetStockTaking);
            await _assetInventoryRegisterRepository.AddRangeAsync(assetStockTakingOrganizations);
            if (!await CommitAsync())
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Handle(CreateAssetInventoryDetailCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var isExist = await _assetInventoryDetailRepository.GetAll(it => it.AssetInventoryRegisterId == request.AssetInventoryRegisterId
                                                               && it.AssetId == request.AssetId).AnyAsync(cancellationToken: cancellationToken);
            if (isExist)
            {
                await Bus.RaiseEventAsync(new DomainNotification("重复", "同一资产多次盘点"));
                return false;
            }
            var detail = new AssetInventoryDetail()
            {
                AssetId = request.AssetId,
                AssetInventoryLocation = request.AssetInventoryLocation,
                AssetInventoryRegisterId = request.AssetInventoryRegisterId,
                ResponsibilityIdentity = request.ResponsibilityIdentity,
                ResponsibilityName = request.ResponsibilityName,
                ResponsibilityOrg2 = request.ResponsibilityOrg2,
                InventoryStatus = request.StockTakingStatus
            };
            await _assetInventoryDetailRepository.AddAsync(detail);
            if (!await CommitAsync())
            {
                return false;
            }
            return true;
        }
    }
}
