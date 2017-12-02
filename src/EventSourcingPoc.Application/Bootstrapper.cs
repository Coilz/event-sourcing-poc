using System;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels;

namespace EventSourcingPoc.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap()
        {
            var eventBus = new EventBus();
            var store = InMemoryEventStore.GetInstance(eventBus);
            Func<IRepository> repositoryProvider = () => new Repository(store);

            var commandHandlerFactory = new CommandHandlerFactory(repositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            var readModelStore = InMemoryReadModelStore.GetInstance();
            Func<IShoppingCartReadModelRepository> readModelRepositoryProvider = () => new ShoppingCartReadModelRepository(readModelStore);

            var eventHandlerFactory = new EventHandlerFactory(repositoryProvider, commandDispatcher, readModelRepositoryProvider);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(readModelRepositoryProvider(), commandDispatcher);
        }

        public class PretendApplication
        {
            private readonly ICommandDispatcher _dispatcher;

            public PretendApplication(IShoppingCartReadModelRepository readModelRepository, ICommandDispatcher dispatcher)
            {
                ReadModelRepository = readModelRepository;
                _dispatcher = dispatcher;
            }

            public IShoppingCartReadModelRepository ReadModelRepository { get; }

            public void Send<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                _dispatcher.Send(cmd);
            }
        }
    }
}
