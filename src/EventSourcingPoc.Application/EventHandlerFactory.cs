using System;
using System.Collections.Generic;
using EventSourcingPoc.Domain.Shipping;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using System.Linq;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Store;
using EventSourcingPoc.Readmodels;
using EventSourcingPoc.EventProcessing;

namespace EventSourcingPoc.Application
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly Dictionary<Type, List<Func<IHandler>>> _handlerFactories = new Dictionary<Type, List<Func<IHandler>>>();

        public EventHandlerFactory(
            Func<IRepository> repositoryProvider,
            ICommandDispatcher dispatcher,
            Func<IShoppingCartReadModelRepository> readModelRepositoryProvider)
        {
            RegisterHandlerFactories(repositoryProvider, dispatcher, readModelRepositoryProvider);
        }

        private void RegisterHandlerFactories(
            Func<IRepository> repositoryProvider,
            ICommandDispatcher dispatcher,
            Func<IShoppingCartReadModelRepository> readModelRepositoryProvider)
        {
            RegisterHandlerFactoryWithTypes(
                () => new ShoppingCartEventHandler(readModelRepositoryProvider()),
                typeof(CartCreated),
                typeof(ProductAddedToCart),
                typeof(ProductRemovedFromCart),
                typeof(CartEmptied),
                typeof(CartCheckedOut));

            RegisterHandlerFactoryWithTypes(
                () => new OrderEventHandler(repositoryProvider(), dispatcher),
                typeof(OrderCreated),
                typeof(PaymentReceived),
                typeof(ShippingAddressConfirmed));
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

            return new List<IEventHandler<TEvent>>();
        }
    }
}
