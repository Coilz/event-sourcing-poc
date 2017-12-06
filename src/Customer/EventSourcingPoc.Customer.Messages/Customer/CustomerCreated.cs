using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerCreated : Event
    {
        public CustomerCreated(Guid aggregateId, CustomerInfo customerInfo)
            : base(aggregateId)
        {
            CustomerInfo = customerInfo;
        }

        public CustomerInfo CustomerInfo { get; }
    }
}
