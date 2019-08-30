using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetExchangeHandledEvent : Event
    {
        public AssetExchange AssetExchange { get; }
        public string Message { get; }
        public AssetExchangeHandledEvent(AssetExchange assetExchange,string message)
        {
            AssetExchange = assetExchange;
            Message = message;
        }       
        public override string ToString()
        {
            return "资产机构简调换已处理";
        }
    }
}