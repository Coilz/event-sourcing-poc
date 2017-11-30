using System;

namespace EventSourcingPoc.Domain.Core.Models
{
    public abstract class Aggregate
    {
        protected Aggregate(Guid id)
        {
            Id = id;
            Version = Guid.NewGuid();
        }

        // Empty constructor for EF
        protected Aggregate() { }

        public Guid Id { get; }
        public Guid Version { get; }
    }
}