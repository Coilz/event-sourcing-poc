using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly EventHandlerFactory _factory;

        public EventDispatcher(EventHandlerFactory factory)
        {
            _factory = factory;
        }
        public async Task SendAsync<TEvent>(TEvent evt)
            where TEvent : Event
        {
            var eventHandlers = _factory.ResolveHandlers<TEvent>();
            var handles = eventHandlers.Select(eventHandler => eventHandler.HandleAsync(evt));
            await Task.WhenAll(handles);
        }
    }
}
