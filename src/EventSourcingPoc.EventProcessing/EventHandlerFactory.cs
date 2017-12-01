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

namespace EventSourcingPoc.EventProcessing
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
                _handlerFactories.Add(type, new List<Func<IHandler>> { handler });
            }
        }

        public IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var evtType = evt.GetType();
            if (_handlerFactories.ContainsKey(evtType))
            {
                var factories = _handlerFactories[evtType];
                return factories.Select(h => (IEventHandler<TEvent>)h());
            }

            return new List<IEventHandler<TEvent>>();
        }
    }
}
