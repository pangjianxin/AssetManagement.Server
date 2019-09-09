using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.AssetCategory
{
    public class CategoryMeteringUnitChangedEvent : Event
    {
        public CategoryMeteringUnitChangedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}