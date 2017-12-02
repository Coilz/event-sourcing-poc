using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Application
{
    public class CommandDispatcher : ICommandDispatcher // TODO: move to CommandProcessing
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }
        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = _factory.Resolve<TCommand>();
            handler.Handle(command);
        }
    }
}
