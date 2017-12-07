using EventSourcingPoc.EventSourcing.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using EventSourcingPoc.Messages;
using System.Threading.Tasks;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public class AggregateRepository : IRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IEventBus _eventBus;
        public AggregateRepository(IEventStore eventStore, IEventBus eventBus)
        {
            _eventStore = eventStore;
            _eventBus = eventBus;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
            where T : EventStream, new()
        {
            var streamItem = new T();
            var streamId = new StreamIdentifier(streamItem.Name, id);
            var history = await _eventStore.GetByStreamIdAsync(streamId);
            streamItem.LoadFromHistory(history);

            return streamItem;
        }

        public async Task SaveAsync(EventStream eventStream)
        {
            var events = eventStream.GetUncommitedChanges().ToList();
            await StoreEventsAsync(eventStream.StreamIdentifier, events);
            await PublishEventsAsync(events);
            eventStream.MarkChangesAsCommitted();
        }

        private async Task StoreEventsAsync(StreamIdentifier id, IEnumerable<Event> events)
        {
            var eventStoreStream = new EventStoreStream(id, events);
            await _eventStore.SaveAsync(eventStoreStream);
        }

        private async Task PublishEventsAsync(IEnumerable<Event> events)
        {
            var notifications = events.Select(@event => _eventBus.NotifySubscribersAsync(@event));
            await Task.WhenAll(notifications);
        }
    }
}
