using Boc.Assets.Domain.Core.Events;
using System;
using Boc.Assets.Domain.Models.Applies;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetApplyRevokedEvent : Event
    {

        public AssetApplyRevokedEvent(Guid aggregateId, AssetApply assetApply, string message)
        {
            AssetCategory = assetApply.AssetCategoryThirdLevel;
            RequestOrgIdentifier = assetApply.RequestOrgIdentifier;
            TargetOrgIdentifier = assetApply.TargetOrgIdentifier;
            Message = message;
            AggregateId = aggregateId;
        }
        public string AssetCategory { get; set; }
        public string RequestOrgIdentifier { get; set; }
        public string TargetOrgIdentifier { get; set; }
        public string Message { get; }
    }
}