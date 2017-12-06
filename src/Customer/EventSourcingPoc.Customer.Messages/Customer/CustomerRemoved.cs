using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerRemoved : Event
    {
        public CustomerRemoved(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
