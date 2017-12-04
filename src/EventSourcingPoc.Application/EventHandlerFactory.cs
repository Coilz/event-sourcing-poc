using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Shipping;
using EventSourcingPoc.Messages.Store;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Readmodels.Store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Application
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly Dictionary<Type, List<Func<IHandler>>> _handlerFactories = new Dictionary<Type, List<Func<IHandler>>>();

        public EventHandlerFactory(
            Func<IRepository> aggregateRepositoryProvider,
            Func<IRepository> sagaRepositoryProvider,
            Func<IShoppingCartReadModelRepository> readModelRepositoryProvider,
            Func<IOrderReadModelRepository> orderReadModelRepositoryProvider
)
        {
            RegisterHandlerFactories(
                aggregateRepositoryProvider,
                sagaRepositoryProvider,
                readModelRepositoryProvider,
                orderReadModelRepositoryProvider);
        }

        private void RegisterHandlerFactories(
            Func<IRepository> aggregateRepositoryProvider,
            Func<IRepository> sagaRepositoryProvider,
            Func<IShoppingCartReadModelRepository> readModelRepositoryProvider,
            Func<IOrderReadModelRepository> orderReadModelRepositoryProvider)
        {
            RegisterHandlerFactoryWithTypes(
                () => new Readmodels.Store.ShoppingCartEventHandler(readModelRepositoryProvider()),
                typeof(CartCreated),
                typeof(ProductAddedToCart),
                typeof(ProductRemovedFromCart),
                typeof(CartEmptied),
                typeof(CartCheckedOut));

            RegisterHandlerFactoryWithTypes(
                () => new EventProcessing.ShoppingCartEventHandler(aggregateRepositoryProvider()),
                typeof(CartCheckedOut));

            RegisterHandlerFactoryWithTypes(
                () => new ShippingEventHandler(sagaRepositoryProvider()),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressConfirmed),
                typeof(PaymentConfirmed),
                typeof(AddressConfirmed));

            RegisterHandlerFactoryWithTypes(
                () => new OrderEventHandler(orderReadModelRepositoryProvider()),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressConfirmed),
                typeof(ShippingProcessStarted),
                typeof(OrderDelivered),
                typeof(OrderCompleted));
        }

        private void RegisterHandlerFactoryWithTypes(Func<IHandler> handler, params Type[] types)
        {
            foreach (var type in types)
            {
                var handlers = new List<Func<IHandler>> { handler };
                if (_handlerFactories.ContainsKey(type))
                    handlers.AddRange(_handlerFactories[type]);

                _handlerFactories[type] = handlers;
            }
        }

        public IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>()
            where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (_handlerFactories.ContainsKey(eventType))
            {
                return _handlerFactories[eventType]
                    .Select(handler => (IEventHandler<TEvent>)handler());
            }

            return Enumerable.Empty<IEventHandler<TEvent>>();
        }
    }
}
