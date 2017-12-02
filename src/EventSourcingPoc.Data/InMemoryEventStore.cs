using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System;

namespace EventSourcingPoc.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private static IEventStore _instance;
        private readonly IEventBus _eventBus;
        private readonly ConcurrentDictionary<string, IEnumerable<IEvent>> _store = new ConcurrentDictionary<string, IEnumerable<IEvent>>();

        public static IEventStore GetInstance(IEventBus eventBus)
        {
            return _instance ?? (_instance = new InMemoryEventStore(eventBus));
        }

        private InMemoryEventStore(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId)
        {
            if (_store.TryGetValue(streamId.Value, out var value))
            {
                return value;
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
            if (_store.TryAdd(eventStoreStream.Id.Value, eventStoreStream.Events)) return;
            if (_store.TryGetValue(eventStoreStream.Id.Value, out var value))
            {
                var currentEvents = value
                    .ToList()
                    .Concat(eventStoreStream.Events);

                if (_store.TryUpdate(eventStoreStream.Id.Value, currentEvents, value)) return;
            }

            throw new InvalidOperationException("Persisting events failed.");
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
