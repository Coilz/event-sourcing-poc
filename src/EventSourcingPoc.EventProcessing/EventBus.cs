using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;

namespace EventSourcingPoc.EventProcessing
{
    public class EventBus : IEventBus
    {
        private readonly List<IEventObserver> _eventObservers = new List<IEventObserver>();

        public Action Subscribe(IEventObserver observer)
        {
            _eventObservers.Add(observer);
            return () => Unsubscribe(observer);
        }

        public void NotifySubscribers(IEvent evt)
        {
            dynamic typeAwareEvent = evt; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
            foreach (var observer in _eventObservers)
            {
                observer.Notify(typeAwareEvent);
            }
        }

        private void Unsubscribe(IEventObserver observer)
        {
            _eventObservers.Remove(observer);
        }
    }
}
