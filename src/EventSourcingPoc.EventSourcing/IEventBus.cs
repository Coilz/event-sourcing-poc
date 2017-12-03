using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing
{
    public interface IEventBus
    {
        Action Subscribe(IEventObserver observer);
        void NotifySubscribers(IEvent evt);
    }
}
