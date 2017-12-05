using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public interface IEventStore
    {
        Task<IEnumerable<IEvent>> GetByStreamIdAsync(StreamIdentifier streamId);
        Task SaveAsync(EventStoreStream eventStoreStream);
        Task SaveAsync(IEnumerable<EventStoreStream> eventStoreStreams);
    }
}
