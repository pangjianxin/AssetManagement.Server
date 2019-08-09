using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetReturnEvent : Event
    {
        public AssetReturnEvent(AssetReturn assetReturn)
        {
            AssetReturn = assetReturn;
        }
        public AssetReturn AssetReturn { get; }

        public override string ToString()
        {
            return "资产交回申请";
        }
    }
}