using System.Collections.Generic;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public interface IEventStore
    {
        IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId);
        void Save(EventStoreStream eventStoreStream);
        void Save(IEnumerable<EventStoreStream> eventStoreStreams);
    }
}
