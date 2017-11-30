using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class CustomerEmailUpdated : IEvent
    {
        public Guid EntityId { get; }
        public string Email { get; }
        public CustomerEmailUpdated(Guid entityId, string email)
        {
            this.Email = email;
            this.EntityId = entityId;
        }
    }
}
