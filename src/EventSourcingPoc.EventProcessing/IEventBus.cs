using EventSourcingPoc.Messages;
using System;

namespace EventSourcingPoc.EventProcessing
{
    public interface IEventBus
    {
        Action Subscribe(IEventObserver observer);
        void NotifySubscribers(IEvent evt);
    }
}
