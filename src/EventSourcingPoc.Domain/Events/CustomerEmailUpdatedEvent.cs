using System;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Events
{
    public class CustomerEmailUpdatedEvent : EntityEvent
    {
        public CustomerEmailUpdatedEvent(Guid entityId, string email)
            : base(entityId)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
