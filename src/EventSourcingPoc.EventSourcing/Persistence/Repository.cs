using EventSourcingPoc.EventSourcing.Domain;
using System;
using System.Linq;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public class Repository : IRepository
    {
        private readonly IEventStore _eventStore;
        public Repository(IEventStore eventStore)
        {
            _eventStore = eventStore;
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
            var eventStoreStreams = streamItems
                .Select(item =>
                    new EventStoreStream(item.StreamIdentifier, item.GetUncommitedChanges()));

            _eventStore.Save(eventStoreStreams);

            foreach (var item in streamItems)
            {
                item.Commit();
            }
        }
    }
}
