﻿using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Shop;
using EventSourcingPoc.Shopping.EventProcessing;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shipping;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Shopping.Application
{
    public static class EventHandlerFactoryRegistration
    {
        public static EventHandlerFactory NewEventHandlerFactory(
            Func<IRepository> aggregateRepositoryProvider,
            Func<IRepository> sagaRepositoryProvider,
            Func<IShoppingCartReadModelRepository> readModelRepositoryProvider,
            Func<IOrderReadModelRepository> orderReadModelRepositoryProvider,
            Func<IContextEventProducer> contextEventProducerProvider)
        {
            var eventHandlerFactory = new EventHandlerFactory();

            eventHandlerFactory.RegisterFactory(
                () => new ShoppingCartEventHandler(readModelRepositoryProvider()),
                typeof(CartCreated),
                typeof(ProductAddedToCart),
                typeof(ProductRemovedFromCart),
                typeof(CartEmptied),
                typeof(CartCheckedOut));

            eventHandlerFactory.RegisterFactory(
                () => new EventProcessing.ContextEventHandler(contextEventProducerProvider()),
                typeof(CompletedForShipping));

            eventHandlerFactory.RegisterFactory(
                () => new ShippingEventHandler(sagaRepositoryProvider()),
                typeof(CartCheckedOut),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressProvided),
                typeof(PaymentConfirmed),
                typeof(AddressConfirmed));

            eventHandlerFactory.RegisterFactory(
                () => new OrderEventHandler(orderReadModelRepositoryProvider()),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressProvided),
                typeof(OrderShipped),
                typeof(OrderDelivered),
                typeof(OrderCompleted));

            return eventHandlerFactory;
        }
    }
}
