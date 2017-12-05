using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Handlers
{
    public interface IEventHandler<in TEvent> : IHandler where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
