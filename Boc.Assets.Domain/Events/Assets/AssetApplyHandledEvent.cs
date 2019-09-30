using Boc.Assets.Domain.Core.Events;
using System;
using Boc.Assets.Domain.Models.Applies;

namespace Boc.Assets.Domain.Events.Assets
{
    /// <summary>
    /// 资产申请事件已处理事件
    /// </summary>
    public class AssetApplyHandledEvent : Event
    {
        public AssetApplyHandledEvent(Guid aggregateId, AssetApply assetApply, string message)
        {
            AggregateId = aggregateId;
            AssetCategory = assetApply.AssetCategoryThirdLevel;
            RequestOrgIdentifier = assetApply.RequestOrgIdentifier;
            TargetOrgIdentifier = assetApply.TargetOrgIdentifier;
            Message = message;
        }
        public string RequestOrgIdentifier { get; set; }
        public string TargetOrgIdentifier { get; set; }
        public string AssetCategory { get; set; }
        public string Message { get; }
    }
}