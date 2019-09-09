using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.Organization
{
    public class OrgPasswordChangedEvent : Event
    {
        public OrgPasswordChangedEvent(Guid aggregateId, string orgName, string orgIdentifier)
        {
            OrgName = orgName;
            OrgIdentifier = orgIdentifier;
            AggregateId = aggregateId;
        }
        public string OrgName { get; set; }
        public string OrgIdentifier { get; set; }
    }
}