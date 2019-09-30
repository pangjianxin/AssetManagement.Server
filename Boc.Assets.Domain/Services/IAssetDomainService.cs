using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Services
{
    public interface IAssetDomainService
    {
        Task<AssetApply> CreateAssetApply(OrganizationInfo principal, OrganizationInfo targetOrg, Guid assetCategoryId, string thirdLevelCategory, string message);

        Task<AssetReturn> CreateAssetReturn(Asset asset, OrganizationInfo principal, OrganizationInfo targetOrg, string message);

        Task<AssetExchange> CreateAssetExchange(Asset asset, OrganizationInfo principal, OrganizationInfo targetOrg, OrganizationInfo exchangeOrg, string message);

        Task HandleAssetReturn(Asset asset, AssetReturn apply, string handleMessage);

        Task HandleAssetApply(Asset asset, AssetApply apply, string handleMessage);

        Task HandleAssetExchange(Asset asset, AssetExchange apply, string handleMessage);

        void RevokeAssetApply(AssetApply apply, string handleMessage);

        void RevokeAssetReturn(AssetReturn apply, string handleMessage);

        void RevokeAssetExchange(AssetExchange apply, string handleMessage);

        void RemoveAssetApply(AssetApply apply);

        void RemoveAssetReturn(Asset asset, AssetReturn apply);

        void RemoveAssetExchange(Asset asset, AssetExchange apply);
    }
}