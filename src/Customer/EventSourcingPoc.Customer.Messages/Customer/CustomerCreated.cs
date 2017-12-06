using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerCreated : IEvent
    {
        public CustomerCreated(Guid aggregateId, CustomerInfo customerInfo)
            : base(aggregateId, 0)
        {
            CustomerInfo = customerInfo;
        }

        public CustomerInfo CustomerInfo { get; }
    }
}
