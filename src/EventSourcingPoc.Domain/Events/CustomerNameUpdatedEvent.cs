using System;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Events
{
    public class CustomerNameUpdatedEvent : EntityEvent
    {
        public CustomerNameUpdatedEvent(Guid entityId, string name)
            : base(entityId)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
