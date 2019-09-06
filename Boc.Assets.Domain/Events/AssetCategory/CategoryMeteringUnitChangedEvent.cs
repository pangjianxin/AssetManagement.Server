using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.AssetCategory
{
    public class CategoryMeteringUnitChangedEvent : Event
    {
        public CategoryMeteringUnitChangedEvent(Guid userId, Guid aggregateId)
        {
            Id = userId;
            AggregateId = aggregateId;
        }
        public Guid Id { get; }
    }
}