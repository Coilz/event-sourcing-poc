using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public class EventStoreStream
    {
        public EventStoreStream(StreamIdentifier identifier, IEnumerable<IEvent> events)
        {
            Id = identifier;
            Events = events.ToList();
        }

        public IEnumerable<IEvent> Events { get; }
        public StreamIdentifier Id { get; }
    }
}
