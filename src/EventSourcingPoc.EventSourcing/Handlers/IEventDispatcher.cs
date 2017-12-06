using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Handlers
{
    public interface IEventDispatcher
    {
        Task SendAsync<TEvent>(TEvent evt)
            where TEvent : Event;
    }
}