using EventSourcingPoc.Messages;
using System;

namespace EventSourcingPoc.EventProcessing
{
    public interface IEventBus // TODO: Move to EventSourcing
    {
        Action Subscribe(IEventObserver observer);
        void NotifySubscribers(IEvent evt);
    }
}
