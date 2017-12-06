using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public class EventStoreStream
    {
        public EventStoreStream(StreamIdentifier identifier, IEnumerable<Event> events)
        {
            Id = identifier;
            Events = events.ToList();
        }

        public IEnumerable<Event> Events { get; }
        public StreamIdentifier Id { get; }
    }
}
