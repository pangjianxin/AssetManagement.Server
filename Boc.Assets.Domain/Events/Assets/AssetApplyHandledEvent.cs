using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    /// <summary>
    /// 资产申请事件已处理事件
    /// </summary>
    public class AssetApplyHandledEvent : Event
    {
        public AssetApply AssetApply { get; }
        public string Message { get; }
        public AssetApplyHandledEvent(AssetApply assetApply, string message)
        {
            AssetApply = assetApply;
            Message = message;
        }
        public override string ToString()
        {
            return "资产申请已处理";
        }
    }
}