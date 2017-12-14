using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.EventProcessing;
using EventSourcingPoc.Logistics.Messages.Shipping;
using EventSourcingPoc.Readmodels.Shipping;
using System;
using EventSourcingPoc.Logistics.Messages.Shipment;

namespace EventSourcingPoc.Logistics.Application
{
    public static class EventHandlerFactoryRegistration
    {
        public static EventHandlerFactory NewEventHandlerFactory(
            Func<IRepository> aggregateRepositoryProvider,
            Func<IShipmentReadModelRepository> readModelRepositoryProvider,
            Func<IContextEventProducer> contextEventProducerProvider)
        {
            var eventHandlerFactory = new EventHandlerFactory();

            eventHandlerFactory.RegisterFactory(
                () => new EventProcessing.ShipmentEventHandler(aggregateRepositoryProvider()),
                typeof(OrderCompletedForShipping));

            eventHandlerFactory.RegisterFactory(
                () => new Readmodels.Shipping.ShipmentEventHandler(readModelRepositoryProvider()),
                typeof(ShipmentCreated),
                typeof(ShipmentStarted),
                typeof(ShipmentDelivered));

            eventHandlerFactory.RegisterFactory(
                () => new ContextEventHandler(contextEventProducerProvider()),
                typeof(ShipmentDelivered));

            return eventHandlerFactory;
        }
    }
}
