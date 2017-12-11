using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Shop;
using EventSourcingPoc.Logistics.EventProcessing;
using EventSourcingPoc.Logistics.Messages.Orders;
using EventSourcingPoc.Logistics.Messages.Shipping;
using EventSourcingPoc.Logistics.Messages.Shop;
using LogisticsCartEventHandler = EventSourcingPoc.Readmodels.Shop.LogisticsCartEventHandler;

namespace EventSourcingPoc.Logistics.Application
{
    public static class EventHandlerFactoryRegistration
    {
        public static EventHandlerFactory NewEventHandlerFactory(
            Func<IRepository> aggregateRepositoryProvider,
            Func<IRepository> sagaRepositoryProvider,
            Func<ILogisticsCartReadModelRepository> readModelRepositoryProvider,
            Func<IOrderReadModelRepository> orderReadModelRepositoryProvider,
            Func<IContextEventProducer> contextEventProducerProvider)
        {
            var eventHandlerFactory = new EventHandlerFactory();

            eventHandlerFactory.RegisterFactory(
                () => new LogisticsCartEventHandler(readModelRepositoryProvider()),
                typeof(CartCreated),
                typeof(ProductAddedToCart),
                typeof(ProductRemovedFromCart),
                typeof(CartEmptied),
                typeof(CartCheckedOut));

            eventHandlerFactory.RegisterFactory(
                () => new EventProcessing.LogisticsCartEventHandler(aggregateRepositoryProvider()),
                typeof(CartCheckedOut));

            eventHandlerFactory.RegisterFactory(
                () => new EventProcessing.ContextEventHandler(contextEventProducerProvider()),
                typeof(ShippingProcessStarted));

            eventHandlerFactory.RegisterFactory(
                () => new ShippingEventHandler(sagaRepositoryProvider()),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressConfirmed),
                typeof(PaymentConfirmed),
                typeof(AddressConfirmed));

            eventHandlerFactory.RegisterFactory(
                () => new OrderEventHandler(orderReadModelRepositoryProvider()),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressConfirmed),
                typeof(ShippingProcessStarted),
                typeof(OrderDelivered),
                typeof(OrderCompleted));

            return eventHandlerFactory;
        }
    }
}
