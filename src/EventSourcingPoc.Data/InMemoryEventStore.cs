using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private static IEventStore _instance;
        private readonly ConcurrentDictionary<string, IEnumerable<Event>> _store = new ConcurrentDictionary<string, IEnumerable<Event>>();

        public static IEventStore GetInstance()
        {
            return _instance ?? (_instance = new InMemoryEventStore());
        }

        private InMemoryEventStore()
        {
        }

        public Task<IEnumerable<Event>> GetByStreamIdAsync(StreamIdentifier streamId)
        {
            if (_store.TryGetValue(streamId.Value, out var value))
            {
                return Task.FromResult(value);
            }

            throw new EventStreamNotFoundException(streamId);
        }

        public Task SaveAsync(EventStoreStream eventStoreStream)
        {
            if (_store.TryAdd(eventStoreStream.Id.Value, eventStoreStream.Events)) return Task.FromResult(0);
            if (_store.TryGetValue(eventStoreStream.Id.Value, out var value))
            {
                var currentEvents = value
                    .Concat(eventStoreStream.Events);

                if (_store.TryUpdate(eventStoreStream.Id.Value, currentEvents.ToList(), value)) return Task.FromResult(0);
            }

            throw new InvalidOperationException("Persisting events failed.");
        }

        public async Task SaveAsync(IEnumerable<EventStoreStream> eventStoreStreams)
        {
            foreach (var eventStoreStream in eventStoreStreams)
            {
                await SaveAsync(eventStoreStream);
            }
        }
    }
}
