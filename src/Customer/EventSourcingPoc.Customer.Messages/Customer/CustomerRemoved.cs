using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerRemoved : IEvent
    {
        public CustomerRemoved(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
