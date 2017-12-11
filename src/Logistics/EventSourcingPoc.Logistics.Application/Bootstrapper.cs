﻿using System.Threading.Tasks;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Shop;

namespace EventSourcingPoc.Logistics.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap()
        {
            var eventBus = EventBus.GetInstance();

            var eventStore = InMemoryEventStore.GetInstance();
            IRepository AggregateRepositoryProvider() =>
                new AggregateRepository(eventStore, eventBus);

            var shoppingCartStore = InMemoryReadModelStore<LogisticsCartReadModel>.GetInstance();
            ILogisticsCartReadModelRepository LogisticsCartReadModelRepositoryProvider() =>
                new LogisticsCartReadModelRepository(shoppingCartStore);

            var orderStore = InMemoryReadModelStore<OrderReadModel>.GetInstance();
            IOrderReadModelRepository OrderReadModelRepositoryProvider() =>
                new OrderReadModelRepository(orderStore);

            var commandHandlerFactory = CommandHandlerFactoryRegistration.NewCommandHandlerFactory(AggregateRepositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            IRepository SagaRepositoryProvider() =>
                new SagaRepository(AggregateRepositoryProvider(), commandDispatcher);

            IContextEventProducer ContextEventProducer() =>
                EventProducerFactory.GetEventProducer();

            var eventHandlerFactory = EventHandlerFactoryRegistration.NewEventHandlerFactory(
                AggregateRepositoryProvider,
                SagaRepositoryProvider,
                LogisticsCartReadModelRepositoryProvider,
                OrderReadModelRepositoryProvider,
                ContextEventProducer);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(
                LogisticsCartReadModelRepositoryProvider(),
                OrderReadModelRepositoryProvider(),
                commandDispatcher);
        }

        public class PretendApplication
        {
            private readonly ICommandDispatcher _commandDispatcher;

            public PretendApplication(
                ILogisticsCartReadModelRepository shoppingCartReadModelRepository,
                IOrderReadModelRepository orderReadModelRepository,
                ICommandDispatcher commandDispatcher)
            {
                LogisticsCartReadModelRepository = shoppingCartReadModelRepository;
                OrderReadModelRepository = orderReadModelRepository;
                _commandDispatcher = commandDispatcher;
            }

            public ILogisticsCartReadModelRepository LogisticsCartReadModelRepository { get; }
            public IOrderReadModelRepository OrderReadModelRepository { get; }

            public async Task SendAsync<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                await _commandDispatcher.SendAsync(cmd);
            }
        }
    }
}
