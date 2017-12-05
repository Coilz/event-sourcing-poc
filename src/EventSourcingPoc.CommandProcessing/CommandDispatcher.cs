using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.CommandProcessing
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        public async Task SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = _factory.Resolve<TCommand>();
            await handler.HandleAsync(command);
        }
    }
}
