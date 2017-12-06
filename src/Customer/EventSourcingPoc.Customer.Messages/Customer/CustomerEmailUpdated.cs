using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerEmailUpdated : IEvent
    {
        public string Email { get; }
        public CustomerEmailUpdated(Guid aggregateId, int version, string email)
            : base(aggregateId, version)
        {
            Email = email;
        }
    }
}
