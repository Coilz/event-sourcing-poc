using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Handlers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}