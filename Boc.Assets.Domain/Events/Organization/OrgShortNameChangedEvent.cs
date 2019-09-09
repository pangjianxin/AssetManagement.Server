using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.Organization
{
    public class OrgShortNameChangedEvent : Event
    {
        public OrgShortNameChangedEvent(Guid aggregateId, string beforeModified, string afterModified)
        {
            BeforeModified = beforeModified;
            AfterModified = afterModified;
            AggregateId = aggregateId;
        }
        public string OrgIdentifier { get; set; }
        public string BeforeModified { get; set; }
        public string AfterModified { get; set; }
    }
}