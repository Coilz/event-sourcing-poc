using System;

namespace EventSourcingPoc.Domain.Core
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = id;
            Version = Guid.NewGuid();
        }

        // Empty constructor for EF
        protected Entity() { }

        public Guid Id { get; }
        public Guid Version { get; }
    }
}
