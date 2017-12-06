using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerCreated : IEvent
    {
        public CustomerCreated(Guid aggregateId, CustomerInfo customerInfo)
        {
            AggregateId = aggregateId;
            CustomerInfo = customerInfo;
        }

        public Guid AggregateId { get; }
        public CustomerInfo CustomerInfo { get; }
    }
}
