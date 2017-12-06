using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing
{
    public interface IEventObserver
    {
        Task NotifyAsync<TEvent>(TEvent evt)
            where TEvent : Event;
    }
}
