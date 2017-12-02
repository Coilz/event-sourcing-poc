using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.CommandProcessing
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : ICommand;
    }
}