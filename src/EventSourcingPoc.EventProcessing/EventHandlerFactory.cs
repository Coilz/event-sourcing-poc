using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public class EventHandlerFactory
    {
        private readonly Dictionary<Type, List<Func<IHandler>>> _handlerFactories = new Dictionary<Type, List<Func<IHandler>>>();

        public void RegisterFactory(Func<IHandler> handler, params Type[] types)
        {
            foreach (var type in types)
            {
                var handlers = new List<Func<IHandler>> { handler };
                if (_handlerFactories.ContainsKey(type))
                    handlers.AddRange(_handlerFactories[type]);

                _handlerFactories[type] = handlers;
            }
        }

        public IEnumerable<IEventHandler<TEvent>> ResolveHandlers<TEvent>()
            where TEvent : Event
        {
            var eventType = typeof(TEvent);
            if (_handlerFactories.ContainsKey(eventType))
            {
                return _handlerFactories[eventType]
                    .Select(handler => (IEventHandler<TEvent>)handler());
            }

            return Enumerable.Empty<IEventHandler<TEvent>>();
        }
    }
}
