using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetApplyEvent : Event
    {
        public AssetApplyEvent(AssetApply assetApply)
        {
            AssetApply = assetApply;
        }
        public AssetApply AssetApply { get; }

        public override string ToString()
        {
            return "资产申请";
        }
    }
}