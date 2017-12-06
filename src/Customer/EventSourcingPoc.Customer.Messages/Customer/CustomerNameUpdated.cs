using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerNameUpdated : IEvent
    {
        public Guid AggregateId { get; }
        public string Name { get; }
        public CustomerNameUpdated(Guid aggregateId, string name)
        {
            Name = name;
            AggregateId = aggregateId;
        }
    }
}
