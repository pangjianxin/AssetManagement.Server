using System;
using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetReturnRemovedEvent : Event
    {

        public AssetReturnRemovedEvent(Guid aggregateId, AssetReturn assetReturn, string message)
        {
            AssetName = assetReturn.AssetName;
            RequestOrgIdentifier = assetReturn.RequestOrgIdentifier;
            TargetOrgIdentifier = assetReturn.TargetOrgIdentifier;
            Message = message;
            AggregateId = aggregateId;
        }
        public string AssetName { get; set; }
        public string RequestOrgIdentifier { get; set; }
        public string TargetOrgIdentifier { get; set; }
        public string Message { get; }
    }
}