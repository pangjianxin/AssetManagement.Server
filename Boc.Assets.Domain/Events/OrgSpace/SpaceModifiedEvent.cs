using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.OrgSpace
{
    public class SpaceModifiedEvent : Event
    {
        public SpaceModifiedEvent(Guid aggregateId, string spaceName)
        {

            SpaceName = spaceName;
            AggregateId = aggregateId;
        }
        public string SpaceName { get; set; }
    }
}