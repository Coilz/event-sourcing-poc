using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
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
