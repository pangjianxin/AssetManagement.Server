using Boc.Assets.Domain.Core.Events;
using System;
using Boc.Assets.Domain.Models.Applies;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetExchangeHandledEvent : Event
    {

        public AssetExchangeHandledEvent(Guid aggregateId, AssetExchange assetExchange, string message)
        {
            AssetName = assetExchange.AssetName;
            RequestOrgIdentifier = assetExchange.RequestOrgIdentifier;
            ExchangeOrgIdentifier = assetExchange.ExchangeOrgIdentifier;
            TargetOrgIdentifier = assetExchange.TargetOrgIdentifier;
            Message = message;
            AggregateId = aggregateId;
        }
        public string AssetName { get; set; }
        public string RequestOrgIdentifier { get; set; }
        public string ExchangeOrgIdentifier { get; set; }
        public string TargetOrgIdentifier { get; set; }
        public string Message { get; }
    }
}