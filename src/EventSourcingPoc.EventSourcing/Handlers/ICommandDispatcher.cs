using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Handlers
{
    public interface ICommandDispatcher
    {
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}