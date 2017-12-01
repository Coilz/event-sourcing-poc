﻿using System;
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
        private readonly Dictionary<string, IEnumerable<IEvent>> store = new Dictionary<string, IEnumerable<IEvent>>();

        private readonly List<IEventObserver> eventObservers = new List<IEventObserver>();

        public IEnumerable<IEvent> GetByStreamId(StreamIdentifier streamId)
        {
            if (store.ContainsKey(streamId.Value))
            {
                return store[streamId.Value];
            }
            throw new EventStreamNotFoundException(streamId);
        }

        public void Save(List<EventStoreStream> newEvents)
        {
            foreach (var eventStoreStream in newEvents)
            {
                this.PersistEvents(eventStoreStream);
                this.DispatchEvents(eventStoreStream.Events);
            }
        }

        private void PersistEvents(EventStoreStream eventStoreStream)
        {
            if(store.ContainsKey(eventStoreStream.Id))
            {
                var currentEvents = store[eventStoreStream.Id].ToList();
                currentEvents.AddRange(eventStoreStream.Events);

                store[eventStoreStream.Id] = currentEvents;
            }
            else
            {
                store.Add(eventStoreStream.Id, eventStoreStream.Events);
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
            foreach(var observer in this.eventObservers)
            {
                observer.Notify(typeAwareEvent);
            }
        }

        public Action Subscribe(IEventObserver observer)
        {
            this.eventObservers.Add(observer);
            return () => this.Unsubscribe(observer);
        }

        private void Unsubscribe(IEventObserver observer)
        {
            eventObservers.Remove(observer);
        }
    }
}
