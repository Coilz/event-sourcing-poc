using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Application
{
    public interface ICommandHandlerFactory // TODO: Move to CommandProcessing
    {
        ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : ICommand;
    }
}