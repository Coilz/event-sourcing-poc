using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Data
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, IEnumerable<IEvent>> _store = new Dictionary<string, IEnumerable<IEvent>>();

        private readonly List<IEventObserver> _eventObservers = new List<IEventObserver>();

        public IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId)
        {
            if (_store.ContainsKey(streamId.Value))
            {
                return _store[streamId.Value];
            }
            throw new EventStreamNotFoundException(streamId);
        }

        public void Save(IEnumerable<EventStoreStream> newEvents)
        {
            foreach (var eventStoreStream in newEvents)
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
            foreach (var evt in newEvents)
            {
                NotifySubscribers(evt);
            }
        }

        private void NotifySubscribers(IEvent evt)
        {
            dynamic typeAwareEvent = evt; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
            foreach(var observer in _eventObservers)
            {
                observer.Notify(typeAwareEvent);
            }
        }

        public Action Subscribe(IEventObserver observer)
        {
            _eventObservers.Add(observer);
            return () => Unsubscribe(observer);
        }

        private void Unsubscribe(IEventObserver observer)
        {
            _eventObservers.Remove(observer);
        }
    }
}
