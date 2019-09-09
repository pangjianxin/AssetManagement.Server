using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Events.Employees
{
    public class EmployeeAddedEvent : Event
    {
        public EmployeeAddedEvent(Guid aggregateId, string employeeName, string identifier)
        {
            AggregateId = aggregateId;
            EmployeeName = employeeName;
            Identifier = identifier;
        }
        public string EmployeeName { get; set; }
        public string Identifier { get; set; }
    }
}