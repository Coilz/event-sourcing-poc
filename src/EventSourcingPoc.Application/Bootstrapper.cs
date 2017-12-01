using System;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap()
        {
            var eventBus = new EventBus();
            var store = new InMemoryEventStore(eventBus);
            Func<IRepository> repositoryProvider = () => new Repository(store);

            var commandHandlerFactory = new CommandHandlerFactory(repositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            var readModelStore = new InMemoryReadModelStore();
            var eventHandlerFactory = new EventHandlerFactory(repositoryProvider, commandDispatcher, readModelStore);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(readModelStore, commandDispatcher);
        }

        public class PretendApplication
        {
            private readonly CommandDispatcher _dispatcher;

            public PretendApplication(InMemoryReadModelStore readModelStore, CommandDispatcher dispatcher)
            {
                ReadModelStore = readModelStore;
                _dispatcher = dispatcher;
            }

            public InMemoryReadModelStore ReadModelStore { get; }

            public void Send<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                _dispatcher.Send(cmd);
            }
        }
    }
}
