using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerRemoved : IEvent
    {
        public CustomerRemoved(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
