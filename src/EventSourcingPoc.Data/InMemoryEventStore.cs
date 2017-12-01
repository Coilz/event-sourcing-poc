using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly IEventBus _eventBus;
        private readonly Dictionary<string, IEnumerable<IEvent>> _store = new Dictionary<string, IEnumerable<IEvent>>();

        public InMemoryEventStore(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId)
        {
            if (_store.ContainsKey(streamId.Value))
            {
                return _store[streamId.Value];
            }

            throw new EventStreamNotFoundException(streamId);
        }

        public void Save(IEnumerable<EventStoreStream> eventStoreStreams)
        {
            foreach (var eventStoreStream in eventStoreStreams)
            {
                PersistEvents(eventStoreStream);
                DispatchEvents(eventStoreStream.Events);
            }
        }

        private void PersistEvents(EventStoreStream eventStoreStream)
        {
            if(_store.ContainsKey(eventStoreStream.Id.Value))
            {
                var currentEvents = _store[eventStoreStream.Id.Value].ToList();
                currentEvents.AddRange(eventStoreStream.Events);

                _store[eventStoreStream.Id.Value] = currentEvents;
            }
            else
            {
                _store.Add(eventStoreStream.Id.Value, eventStoreStream.Events);
            }
        }

        private void DispatchEvents(IEnumerable<IEvent> newEvents)
        {
            foreach (var newEvent in newEvents)
            {
                _eventBus.NotifySubscribers(newEvent);
            }
        }
    }
}
