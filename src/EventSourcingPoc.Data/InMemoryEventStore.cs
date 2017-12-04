using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private static IEventStore _instance;
        private readonly ConcurrentDictionary<string, IEnumerable<IEvent>> _store = new ConcurrentDictionary<string, IEnumerable<IEvent>>();

        public static IEventStore GetInstance()
        {
            return _instance ?? (_instance = new InMemoryEventStore());
        }

        private InMemoryEventStore()
        {
        }

        public IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId)
        {
            if (_store.TryGetValue(streamId.Value, out var value))
            {
                return value;
            }

            throw new EventStreamNotFoundException(streamId);
        }

        public void Save(EventStoreStream eventStoreStream)
        {
            if (_store.TryAdd(eventStoreStream.Id.Value, eventStoreStream.Events)) return;
            if (_store.TryGetValue(eventStoreStream.Id.Value, out var value))
            {
                var currentEvents = value
                    .Concat(eventStoreStream.Events);

                if (_store.TryUpdate(eventStoreStream.Id.Value, currentEvents.ToList(), value)) return;
            }

            throw new InvalidOperationException("Persisting events failed.");
        }

        public void Save(IEnumerable<EventStoreStream> eventStoreStreams)
        {
            foreach (var eventStoreStream in eventStoreStreams)
            {
                Save(eventStoreStream);
            }
        }
    }
}
