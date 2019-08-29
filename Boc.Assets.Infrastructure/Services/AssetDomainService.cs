using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using System;

namespace Boc.Assets.Infrastructure.Services
{
    public class AssetDomainService : IAssetDomainService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetDomainService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }
        public AssetDeploy HandleAssetReturn(Asset asset, AssetReturn @event)
        {
            asset.LastModifyDateTime = DateTime.Now;
            asset.LastModifyComment = @event.Message;
            asset.AssetStatus = AssetStatus.在库;
            asset.AssetLocation = @event.RequestOrgNam;
            asset.StoredOrgIdentifier = @event.TargetOrgIdentifier;
            asset.StoredOrgName = @event.TargetOrgNam;
            return new AssetDeploy
            {
                Id = Guid.NewGuid(),
                AssetDeployCategory = AssetDeployCategory.资产交回,
                CreateDateTime = DateTime.Now,
                AssetTagNumber = asset.AssetTagNumber,
                AssetName = asset.AssetName,
                ExportOrgInfo = new OrganizationInfo()
                {
                    Org2 = @event.Org2,
                    OrgId = @event.RequestOrgId,
                    OrgIdentifier = @event.RequestOrgIdentifier,
                    OrgNam = @event.RequestOrgNam
                },
                ImportOrgInfo = new OrganizationInfo()
                {
                    OrgId = @event.TargetOrgId,
                    OrgIdentifier = @event.TargetOrgIdentifier,
                    OrgNam = @event.TargetOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo()
                {
                    OrgId = @event.TargetOrgId,
                    OrgIdentifier = @event.TargetOrgIdentifier,
                    OrgNam = @event.TargetOrgNam
                }
            };
        }

        public AssetDeploy HandleAssetApplying(Asset asset, AssetApply @event)
        {
            asset.LastModifyDateTime = DateTime.Now;
            asset.LastModifyComment = $"资产由{@event.TargetOrgIdentifier}调至{@event.RequestOrgIdentifier}";
            asset.AssetStatus = AssetStatus.在用;
            asset.AssetLocation = @event.RequestOrgNam;
            asset.StoredOrgIdentifier = @event.TargetOrgIdentifier;
            asset.StoredOrgName = @event.TargetOrgNam;
            return new AssetDeploy
            {
                Id = Guid.NewGuid(),
                AssetDeployCategory = AssetDeployCategory.资产申请,
                CreateDateTime = DateTime.Now,
                AssetTagNumber = asset.AssetTagNumber,
                AssetName = asset.AssetName,
                ExportOrgInfo = new OrganizationInfo
                {
                    Org2 = @event.Org2,
                    OrgId = @event.TargetOrgId,
                    OrgIdentifier = @event.TargetOrgIdentifier,
                    OrgNam = @event.TargetOrgNam
                },
                ImportOrgInfo = new OrganizationInfo
                {
                    Org2 = @event.Org2,
                    OrgId = @event.RequestOrgId,
                    OrgIdentifier = @event.RequestOrgIdentifier,
                    OrgNam = @event.RequestOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo
                {
                    Org2 = @event.Org2,
                    OrgId = @event.TargetOrgId,
                    OrgIdentifier = @event.TargetOrgIdentifier,
                    OrgNam = @event.TargetOrgNam
                }
            };
        }

        public AssetDeploy HandleAssetExchanging(Asset asset, AssetExchange @event)
        {
            asset.LastModifyDateTime = DateTime.Now;
            asset.LastModifyComment = $"资产由{@event.RequestOrgIdentifier}调配至{@event.ExchangeOrgIdentifier}";
            asset.AssetStatus = AssetStatus.在用;
            asset.AssetLocation = @event.ExchangeOrgNam;
            asset.StoredOrgIdentifier = @event.ExchangeOrgIdentifier;
            asset.StoredOrgName = @event.ExchangeOrgNam;
            return new AssetDeploy()
            {
                Id = Guid.NewGuid(),
                AssetDeployCategory = AssetDeployCategory.资产机构间调配,
                CreateDateTime = DateTime.Now,
                AssetTagNumber = asset.AssetTagNumber,
                AssetName = asset.AssetName,
                ExportOrgInfo = new OrganizationInfo
                {
                    Org2 = @event.Org2,
                    OrgId = @event.RequestOrgId,
                    OrgIdentifier = @event.RequestOrgIdentifier,
                    OrgNam = @event.RequestOrgNam
                },
                ImportOrgInfo = new OrganizationInfo
                {
                    Org2 = @event.Org2,
                    OrgId = @event.ExchangeOrgId,
                    OrgIdentifier = @event.ExchangeOrgIdentifier,
                    OrgNam = @event.ExchangeOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo
                {
                    Org2 = @event.Org2,
                    OrgId = @event.TargetOrgId,
                    OrgIdentifier = @event.TargetOrgIdentifier,
                    OrgNam = @event.TargetOrgNam
                }
            };
        }
    }
}