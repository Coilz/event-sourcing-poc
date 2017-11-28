using System;

namespace EventSourcingPoc.Domain.Core.Events
{
    public abstract class EntityEvent : Event
    {
        protected EntityEvent(Guid entityId)
        {
            EntityId = entityId;
        }

        public Guid EntityId { get; }

    }
}
