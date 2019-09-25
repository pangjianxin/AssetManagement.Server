using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.Organization
{
    public class OrgPasswordResetEvent : Event
    {
        public OrgPasswordResetEvent(Guid aggregateId, string orgName, string orgIdentifier)
        {
            AggregateId = aggregateId;
            OrgName = orgName;
            OrgIdentifier = orgIdentifier;
        }
        public string OrgName { get; set; }
        public string OrgIdentifier { get; set; }
    }
}