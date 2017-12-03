using EventSourcingPoc.EventSourcing.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using EventSourcingPoc.Messages;

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

        public T GetById<T>(Guid id)
            where T : EventStream, new()
        {
            var streamItem = new T();
            var streamId = new StreamIdentifier(streamItem.Name, id);
            var history = _eventStore.GetByStreamId(streamId);
            streamItem.LoadFromHistory(history);

            return streamItem;
        }

        public void Save(params EventStream[] streamItems)
        {
            foreach (var item in streamItems)
            {
                Save(item);
            }
        }

        protected virtual void Save(EventStream eventStream)
        {
            var events = eventStream.GetUncommitedChanges();
            StoreEvents(eventStream.StreamIdentifier, events);
            PublishEvents(events);
            eventStream.MarkChangesAsCommitted();
        }

        private void StoreEvents(StreamIdentifier id, IEnumerable<IEvent> events)
        {
            var eventStoreStream = new EventStoreStream(id, events);
            _eventStore.Save(eventStoreStream);
        }

        private void PublishEvents(IEnumerable<IEvent> events)
        {
            foreach (var newEvent in events)
            {
                _eventBus.NotifySubscribers(newEvent);
            }
        }
    }
}
