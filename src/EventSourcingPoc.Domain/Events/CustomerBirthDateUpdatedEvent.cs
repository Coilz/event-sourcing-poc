using System;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Events
{
    public class CustomerBirthDateUpdatedEvent : EntityEvent
    {
        public CustomerBirthDateUpdatedEvent(Guid entityId, DateTime birthDate)
            : base(entityId)
        {
            BirthDate = birthDate;
        }

        public DateTime BirthDate { get; }
    }
}
