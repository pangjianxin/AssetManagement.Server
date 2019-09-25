using Boc.Assets.Domain.Commands.AssetStockTaking;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.AssetStockTakings
{
    public class AssetStockTakingCommandHandler : CommandHandler,
        IRequestHandler<CreateAssetStockTakingCommand, bool>,
        IRequestHandler<CreateAssetStockTakingDetailCommand, bool>
    {
        private readonly IAssetStockTakingRepository _assetStockTakingRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetStockTakingOrganizationRepository _assetStockTakingOrganizationRepository;
        private readonly IAssetStockTakingDetailRepository _detailRepository;
        private readonly IUser _user;

        public AssetStockTakingCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IAssetStockTakingRepository assetStockTakingRepository,
            IOrganizationRepository organizationRepository,
            IAssetStockTakingOrganizationRepository assetStockTakingOrganizationRepository,
            IAssetStockTakingDetailRepository detailRepository,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetStockTakingRepository = assetStockTakingRepository;
            _organizationRepository = organizationRepository;
            _assetStockTakingOrganizationRepository = assetStockTakingOrganizationRepository;
            _detailRepository = detailRepository;
            _user = user;
        }

        public async Task<bool> Handle(CreateAssetStockTakingCommand request, CancellationToken cancellationToken)
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

            var assetStockTaking = new AssetStockTaking(publisher, request.TaskName, request.TaskComment, request.ExpiryDateTime);
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

            var assetStockTakingOrganizations = new List<AssetStockTakingOrganization>();
            foreach (var targetOrganization in targetOrganizations)
            {
                assetStockTakingOrganizations.Add(new AssetStockTakingOrganization(targetOrganization.Id, assetStockTaking.Id));
            }

            await _assetStockTakingRepository.AddAsync(assetStockTaking);
            await _assetStockTakingOrganizationRepository.AddRangeAsync(assetStockTakingOrganizations);
            if (!await CommitAsync())
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Handle(CreateAssetStockTakingDetailCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var isExist = await _detailRepository.GetAll(it => it.AssetStockTakingOrganizationId == request.AssetStockTakingOrganizationId
                                                               && it.AssetId == request.AssetId).AnyAsync(cancellationToken: cancellationToken);
            if (isExist)
            {
                await Bus.RaiseEventAsync(new DomainNotification("重复", "同一资产多次盘点"));
                return false;
            }
            var detail = new AssetStockTakingDetail()
            {
                AssetId = request.AssetId,
                AssetStockTakingLocation = request.AssetStockTakingLocation,
                AssetStockTakingOrganizationId = request.AssetStockTakingOrganizationId,
                ResponsibilityIdentity = request.ResponsibilityIdentity,
                ResponsibilityName = request.ResponsibilityName,
                ResponsibilityOrg2 = request.ResponsibilityOrg2,
                StockTakingStatus = request.StockTakingStatus
            };
            await _detailRepository.AddAsync(detail);
            if (!await CommitAsync())
            {
                return false;
            }
            return true;
        }
    }
}
