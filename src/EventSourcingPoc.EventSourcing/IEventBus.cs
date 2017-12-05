using System;
using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing
{
    public interface IEventBus
    {
        Action Subscribe(IEventObserver observer);
        Task NotifySubscribersAsync(IEvent evt);
    }
}
