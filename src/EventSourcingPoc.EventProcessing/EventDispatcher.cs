using System.Linq;
using System.Threading.Tasks;
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
        public async Task SendAsync<TEvent>(TEvent evt)
            where TEvent : IEvent
        {
            var eventHandlers = _factory.Resolve<TEvent>();
            var handles = eventHandlers.Select(eventHandler => eventHandler.HandleAsync(evt));
            await Task.WhenAll(handles);
        }
    }
}
