using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EventSourcingPoc.EventProcessing
{
    public class EventBus : IEventBus
    {
        private static IEventBus _instance;
        public static IEventBus GetInstance()
        {
            return _instance ?? (_instance = new EventBus());
        }

        private readonly List<IEventObserver> _eventObservers = new List<IEventObserver>();
        private EventBus() {}

        public Action Subscribe(IEventObserver observer)
        {
            _eventObservers.Add(observer);
            return () => Unsubscribe(observer);
        }

        public async Task NotifySubscribersAsync(Event evt)
        {
            dynamic typeAwareEvent = evt; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
            var notifiers = _eventObservers.Select(observer => (Task)observer.NotifyAsync(typeAwareEvent));
            await Task.WhenAll(notifiers);
        }

        private void Unsubscribe(IEventObserver observer)
        {
            _eventObservers.Remove(observer);
        }
    }
}
