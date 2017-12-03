using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Handlers
{
    public interface ICommandHandler<in TCommand> : IHandler
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
