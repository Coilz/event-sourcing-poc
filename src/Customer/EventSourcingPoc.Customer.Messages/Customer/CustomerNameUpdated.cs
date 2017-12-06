using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerNameUpdated : Event
    {
        public string Name { get; }

        public CustomerNameUpdated(Guid aggregateId, int version, string name)
            : base(aggregateId, version)
        {
            Name = name;
        }
    }
}
