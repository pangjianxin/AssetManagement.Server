using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetExchangeHandledEvent : Event
    {
        public AssetExchangeHandledEvent(AssetExchange assetExchange)
        {
            AssetExchange = assetExchange;
        }

        public AssetExchange AssetExchange { get; }
    }
}