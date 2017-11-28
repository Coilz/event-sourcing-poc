using System.Threading.Tasks;
using EventSourcingPoc.Domain.Core.Commands;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Core
{
    public interface IBus
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
