using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Assets
{
    public class AssetsCommandHandler : CommandHandler,
        IRequestHandler<ModifyAssetLocationCommand, bool>,
        IRequestHandler<StoreAssetWithOutFileCommand, bool>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IUser _user;

        public AssetsCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IAssetRepository assetRepository,
            IAssetCategoryRepository assetCategoryRepository,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetRepository = assetRepository;
            _assetCategoryRepository = assetCategoryRepository;
            _user = user;
        }

        public async Task<bool> Handle(ModifyAssetLocationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var asset = await _assetRepository.GetByIdAsync(request.AssetId);
            var beforeChangedLocation = asset.AssetLocation;
            var afterChangedLocation = asset.ModifyAssetLocation(request.AssetLocation);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetLocationChangedEvent(_user.OrgId, beforeChangedLocation, afterChangedLocation));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(StoreAssetWithOutFileCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var category = await _assetCategoryRepository.GetByIdAsync(request.AssetCategoryId);
            if (category == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "未找到相关分类，请联系管理员"));
                return false;
            }

            var tagNumberPrefix = request.StartTagNumber.Substring(0, 10);
            var startCountParseResult = int.TryParse(request.StartTagNumber.Substring(10, 5), out var startCount);
            if (!startCountParseResult)
            {
                await Bus.RaiseEventAsync(new DomainNotification("操作错误", "录入的起始标签号有误，请重新填写"));
                return false;
            }
            var endCountParseResult = int.TryParse(request.EndTagNumber.Substring(10, 5), out var endCount);
            if (!endCountParseResult)
            {
                await Bus.RaiseEventAsync(new DomainNotification("操作错误", "录入的结束标签号有误，请重新填写"));
                return false;
            }
            //临时存放入库资产的列表
            var tempInventoryList = new List<Asset>();
            for (var operationCount = startCount; operationCount <= endCount; operationCount++)
            {
                var tagNumber = new StringBuilder();
                var tagNumberSuffix = new StringBuilder(operationCount.ToString());
                while (tagNumberSuffix.Length != 5)
                {
                    tagNumberSuffix.Insert(0, '0');
                }

                tagNumber.Append(tagNumberPrefix);
                tagNumber.Append(tagNumberSuffix);
                var asset = new Asset
                {
                    AssetStatus = AssetStatus.在库,
                    Id = Guid.NewGuid(),
                    AssetLocation = request.AssetLocation,
                    AssetCategoryId = category.Id,
                    AssetName = request.AssetName,
                    Brand = request.Brand,
                    AssetDescription = request.AssetDescription,
                    AssetType = request.AssetType,
                    AssetTagNumber = tagNumber.ToString(),
                    InStoreDateTime = DateTime.Now,
                    LastModifyDateTime = DateTime.Now,
                    LastDeployRecord = request.Message,
                    CreateDateTime = request.CreateDateTime,
                    OrganizationInChargeId = _user.OrgId,
                    OrgInUseIdentifier = _user.OrgIdentifier,
                    OrgInUseName = _user.OrgNam
                };
                tempInventoryList.Add(asset);
            }
            await _assetRepository.AddRangeAsync(tempInventoryList);

            if (await CommitAsync())
            {
                return true;
            }
            return false;
        }
    }
}