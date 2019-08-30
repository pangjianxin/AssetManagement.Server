using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetReturnHandledEvent : Event
    {
        public AssetReturn AssetReturn { get; }
        public string Message { get; }
        public AssetReturnHandledEvent(AssetReturn assetReturn, string message)
        {
            AssetReturn = assetReturn;
            Message = message;
        }

        public override string ToString()
        {
            return "资产交回已处理";
        }
    }
}