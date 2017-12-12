using System.Threading.Tasks;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Shop;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Shopping.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap(ILogger logger)
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

            var commandHandlerFactory = CommandHandlerFactoryRegistration.NewCommandHandlerFactory(AggregateRepositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            IRepository SagaRepositoryProvider() =>
                new SagaRepository(AggregateRepositoryProvider(), commandDispatcher);

            IContextEventProducer ContextEventProducer() =>
                EventProducerFactory.GetEventProducer(logger);

            var eventHandlerFactory = EventHandlerFactoryRegistration.NewEventHandlerFactory(
                AggregateRepositoryProvider,
                SagaRepositoryProvider,
                ShoppingCartReadModelRepositoryProvider,
                OrderReadModelRepositoryProvider,
                ContextEventProducer);
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

            public async Task SendAsync<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                await _commandDispatcher.SendAsync(cmd);
            }
        }
    }
}
