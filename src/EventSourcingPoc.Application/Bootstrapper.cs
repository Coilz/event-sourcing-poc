using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Store;

namespace EventSourcingPoc.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap()
        {
            var eventBus = EventBus.GetInstance();

            var eventStore = InMemoryEventStore.GetInstance();
            IRepository AggregateRepositoryProvider() =>
                new AggregateRepository(eventStore, eventBus);

            var shoppingCartStore = InMemoryReadModelStore<ShoppingCartReadModel>.GetInstance();
            IShoppingCartReadModelRepository ShoppingCartReadModelRepositoryProvider() => 
                new ShoppingCartReadModelRepository(shoppingCartStore);

            var orderStore = InMemoryReadModelStore<OrderReadModel>.GetInstance();
            IOrderReadModelRepository OrderReadModelRepositoryProvider() =>
                new OrderReadModelRepository(orderStore);

            var commandHandlerFactory = new CommandHandlerFactory(AggregateRepositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            IRepository SagaRepositoryProvider() =>
                new SagaRepository(AggregateRepositoryProvider(), commandDispatcher);

            var eventHandlerFactory = new EventHandlerFactory(
                AggregateRepositoryProvider,
                SagaRepositoryProvider,
                ShoppingCartReadModelRepositoryProvider,
                OrderReadModelRepositoryProvider);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(
                ShoppingCartReadModelRepositoryProvider(),
                OrderReadModelRepositoryProvider(),
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
