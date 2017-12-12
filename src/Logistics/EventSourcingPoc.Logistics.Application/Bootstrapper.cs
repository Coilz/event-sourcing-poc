using System;
using System.Threading.Tasks;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Kafka;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Shipping;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Logistics.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap(ILogger logger)
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
                EventProducerFactory.GetEventProducer(logger);

            var eventHandlerFactory = EventHandlerFactoryRegistration.NewEventHandlerFactory(
                AggregateRepositoryProvider,
                ShipmentReadModelRepositoryProvider,
                ContextEventProducer);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            var eventConsumer = EventConsumerFactory.GetEventConsumer(eventDispatcher, logger);

            return new PretendApplication(
                ShipmentReadModelRepositoryProvider(),
                commandDispatcher,
                eventConsumer);
        }

        public class PretendApplication : IDisposable
        {
            private readonly ICommandDispatcher _commandDispatcher;

            public PretendApplication(
                IShipmentReadModelRepository shipmentReadModelRepository,
                ICommandDispatcher commandDispatcher,
                EventConsumer eventConsumer)
            {
                ShipmentReadModelRepository = shipmentReadModelRepository;
                _commandDispatcher = commandDispatcher;
                EventConsumer = eventConsumer;
            }

            public IShipmentReadModelRepository ShipmentReadModelRepository { get; }
            public EventConsumer EventConsumer { get; private set; }

            public async Task SendAsync<TCommand>(TCommand cmd)
                where TCommand : ICommand
            {
                await _commandDispatcher.SendAsync(cmd);
            }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        EventConsumer.Dispose();
                        EventConsumer = null;
                    }

                    disposedValue = true;
                }
            }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
            }
            #endregion
        }
    }
}
