using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Handlers
{
    public interface ICommandHandler<in TCommand> : IHandler
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
