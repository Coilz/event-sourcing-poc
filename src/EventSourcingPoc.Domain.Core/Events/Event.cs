using System;

namespace EventSourcingPoc.Domain.Core.Events
{
    public abstract class Event : Message
    {
        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; }
    }
}
