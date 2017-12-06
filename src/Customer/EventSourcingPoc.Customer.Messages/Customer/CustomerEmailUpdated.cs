using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerEmailUpdated : IEvent
    {
        public Guid AggregateId { get; }
        public string Email { get; }
        public CustomerEmailUpdated(Guid aggregateId, string email)
        {
            Email = email;
            AggregateId = aggregateId;
        }
    }
}
