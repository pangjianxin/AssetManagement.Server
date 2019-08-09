using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetReturnRevokedEvent : Event
    {
        public AssetReturnRevokedEvent(AssetReturn assetReturn, string message)
        {
            AssetReturn = assetReturn;
            Message = message;
        }
        public AssetReturn AssetReturn { get; }
        public string Message { get; }
    }
}