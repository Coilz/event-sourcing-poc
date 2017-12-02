using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEventHandlerFactory _factory;

        public EventDispatcher(IEventHandlerFactory factory)
        {
            _factory = factory;
        }
        public void Send<TEvent>(TEvent evt)
            where TEvent : IEvent
        {
            var handlers = _factory.Resolve<TEvent>();
            foreach (var eventHandler in handlers)
            {
                eventHandler.Handle(evt);
            }
        }
    }
}