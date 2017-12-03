using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Store;
using System;

namespace EventSourcingPoc.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap()
        {
            var eventBus = EventBus.GetInstance();

            var eventStore = InMemoryEventStore.GetInstance();
            Func<IRepository> repositoryProvider = () => new AggregateRepository(eventStore, eventBus);

            var shoppingCartStore = InMemoryReadModelStore<ShoppingCartReadModel>.GetInstance();
            Func<IShoppingCartReadModelRepository> shoppingCartReadModelRepositoryProvider = () =>
                new ShoppingCartReadModelRepository(shoppingCartStore);

            var orderStore = InMemoryReadModelStore<OrderReadModel>.GetInstance();
            Func<IOrderReadModelRepository> orderReadModelRepositoryProvider = () =>
                new OrderReadModelRepository(orderStore);

            var commandHandlerFactory = new CommandHandlerFactory(repositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            var eventHandlerFactory = new EventHandlerFactory(
                repositoryProvider,
                shoppingCartReadModelRepositoryProvider,
                orderReadModelRepositoryProvider,
                commandDispatcher);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(
                shoppingCartReadModelRepositoryProvider(),
                orderReadModelRepositoryProvider(),
                commandDispatcher);
        }

        public class PretendApplication
        {
            private readonly ICommandDispatcher _commandDispatcher;

            public PretendApplication(
                IShoppingCartReadModelRepository shoppingCartReadModelRepository,
                IOrderReadModelRepository orderReadModelRepository,
                ICommandDispatcher commandDispatcher)
            {
                ShoppingCartReadModelRepository = shoppingCartReadModelRepository;
                OrderReadModelRepository = orderReadModelRepository;
                _commandDispatcher = commandDispatcher;
            }

            public IShoppingCartReadModelRepository ShoppingCartReadModelRepository { get; }
            public IOrderReadModelRepository OrderReadModelRepository { get; }

            public void Send<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                _commandDispatcher.Send(cmd);
            }
        }
    }
}
