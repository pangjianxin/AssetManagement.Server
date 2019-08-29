using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Services
{
    public interface IAssetDomainService
    {
        AssetDeploy HandleAssetReturn(Asset asset, AssetReturn @event);
        AssetDeploy HandleAssetApplying(Asset asset, AssetApply @event);
        AssetDeploy HandleAssetExchanging(Asset asset, AssetExchange @event);
    }
}