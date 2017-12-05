using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerCreated : IEvent
    {
        public CustomerCreated(Guid id, CustomerInfo customerInfo)
        {
            Id = id;
            CustomerInfo = customerInfo;
        }

        public Guid Id { get; }
        public CustomerInfo CustomerInfo { get; }
    }
}
