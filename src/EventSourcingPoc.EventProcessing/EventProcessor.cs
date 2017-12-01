using EventSourcingPoc.Data;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public class EventProcessor : IEventObserver
    {
        private readonly IEventDispatcher _dispatcher;

        public EventProcessor(InMemoryEventStore store, IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            store.Subscribe(this);
        }

        public void Notify<TEvent>(TEvent evt) where TEvent : IEvent
        {
            _dispatcher.Send(evt);
        }
    }
}
