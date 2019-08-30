using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetApplyRemovedEvent : Event
    {
        public AssetApply AssetApply { get; }
        public string Message { get; }

        public AssetApplyRemovedEvent(AssetApply assetApply, string message)
        {
            AssetApply = assetApply;
            Message = message;
        }

        public override string ToString()
        {
            return "资产申请已删除";
        }
    }
}