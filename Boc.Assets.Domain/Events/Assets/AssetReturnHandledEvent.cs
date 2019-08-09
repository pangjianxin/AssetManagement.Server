using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetReturnHandledEvent : Event
    {
        public AssetReturnHandledEvent(AssetReturn assetReturn)
        {
            AssetReturn = assetReturn;
        }

        public AssetReturn AssetReturn { get; }
    }
}