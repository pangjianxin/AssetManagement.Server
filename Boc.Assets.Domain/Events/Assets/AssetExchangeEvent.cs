using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetExchangeEvent : Event
    {
        public AssetExchangeEvent(AssetExchange assetExchange)
        {
            AssetExchange = assetExchange;
        }
        public AssetExchange AssetExchange { get; }

        public override string ToString()
        {
            return "资产机构间调换";
        }
    }
}