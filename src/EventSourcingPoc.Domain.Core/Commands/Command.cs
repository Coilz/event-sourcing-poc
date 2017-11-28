using System;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; }
    }
}
