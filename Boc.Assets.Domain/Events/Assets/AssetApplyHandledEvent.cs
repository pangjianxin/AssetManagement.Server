using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    /// <summary>
    /// 资产申请事件已处理事件
    /// </summary>
    public class AssetApplyHandledEvent : Event
    {
        public AssetApplyHandledEvent(AssetApply assetApply)
        {
            AssetApply = assetApply;
        }
        public AssetApply AssetApply { get; }
    }
}