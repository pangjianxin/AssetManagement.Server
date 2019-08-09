using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetExchangeRevokedEvent : Event
    {
        public AssetExchangeRevokedEvent(AssetExchange assetExchange, string message)
        {
            AssetExchange = assetExchange;
            Message = message;
        }

        public AssetExchange AssetExchange { get; }
        public string Message { get; }
    }
}