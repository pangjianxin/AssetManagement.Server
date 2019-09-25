using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetLocationChangedEvent : Event
    {
        public AssetLocationChangedEvent(Guid aggregateId, string beforeChanged, string afterChanged)
        {
            AggregateId = aggregateId;
            BeforeChanged = beforeChanged;
            AfterChanged = afterChanged;
        }
        public string BeforeChanged { get; set; }
        public string AfterChanged { get; set; }
    }
}