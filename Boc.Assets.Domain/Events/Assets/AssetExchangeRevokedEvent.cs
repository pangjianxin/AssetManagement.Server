using System;
using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetExchangeRevokedEvent : Event
    {
        
        public AssetExchangeRevokedEvent(Guid aggregateId, AssetExchange assetExchange, string message)
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
        public override string ToString()
        {
            return "资产机构简调换已撤销";
        }
    }
}