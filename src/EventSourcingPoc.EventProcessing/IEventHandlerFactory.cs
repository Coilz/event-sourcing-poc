using System.Collections.Generic;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}