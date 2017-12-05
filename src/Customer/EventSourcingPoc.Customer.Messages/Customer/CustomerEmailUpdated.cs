using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerEmailUpdated : IEvent
    {
        public Guid CustomerId { get; }
        public string Email { get; }
        public CustomerEmailUpdated(Guid id, string email)
        {
            Email = email;
            CustomerId = id;
        }
    }
}
