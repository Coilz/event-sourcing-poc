using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public class EventProcessor : IEventObserver
    {
        private readonly IEventDispatcher _dispatcher;

        public EventProcessor(IEventBus eventBus, IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            eventBus.Subscribe(this);
        }

        public void Notify<TEvent>(TEvent evt) where TEvent : IEvent
        {
            _dispatcher.Send(evt);
        }
    }
}
