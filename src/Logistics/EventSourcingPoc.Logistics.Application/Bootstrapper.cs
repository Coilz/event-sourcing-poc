using System.Threading.Tasks;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Shipping;

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

            var shipmentStore = InMemoryReadModelStore<ShipmentReadModel>.GetInstance();
            IShipmentReadModelRepository ShipmentReadModelRepositoryProvider() =>
                new ShipmentReadModelRepository(shipmentStore);

            var commandHandlerFactory = CommandHandlerFactoryRegistration.NewCommandHandlerFactory(AggregateRepositoryProvider);
            var commandDispatcher = new CommandDispatcher(commandHandlerFactory);

            IContextEventProducer ContextEventProducer() =>
                EventProducerFactory.GetEventProducer();

            var eventHandlerFactory = EventHandlerFactoryRegistration.NewEventHandlerFactory(
                AggregateRepositoryProvider,
                ShipmentReadModelRepositoryProvider,
                ContextEventProducer);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(
                ShipmentReadModelRepositoryProvider(),
                commandDispatcher);
        }

        public class PretendApplication
        {
            private readonly ICommandDispatcher _commandDispatcher;

            public PretendApplication(
                IShipmentReadModelRepository shipmentReadModelRepository,
                ICommandDispatcher commandDispatcher)
            {
                ShipmentReadModelRepository = shipmentReadModelRepository;
                _commandDispatcher = commandDispatcher;
            }

            public IShipmentReadModelRepository ShipmentReadModelRepository { get; }

            public async Task SendAsync<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                await _commandDispatcher.SendAsync(cmd);
            }
        }
    }
}
