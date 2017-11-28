using System;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Events
{
    public class CustomerRegisteredEvent : EntityEvent
    {
        public CustomerRegisteredEvent(Guid entityId, string name, string email, DateTime birthDate)
            : base(entityId)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public string Name { get; }

        public string Email { get; }

        public DateTime BirthDate { get; }
    }
}
