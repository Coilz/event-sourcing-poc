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
        private readonly Dictionary<Type, List<Func<IHandler>>> handlerFactories = new Dictionary<Type, List<Func<IHandler>>>();

        public EventHandlerFactory(IEventStore eventStore, ICommandDispatcher dispatcher, IShoppingCartReadModelRepository mongo)
        {
            RegisterHandlerFactories(eventStore,dispatcher,mongo);
        }

        private void RegisterHandlerFactories(IEventStore eventStore, ICommandDispatcher dispatcher, IShoppingCartReadModelRepository mongo)
        {
            this.RegisterHandlerFactoryWithTypes(
                () => new ShoppingCartEventHandler(mongo),
                typeof(CartCreated), typeof(ProductAddedToCart), typeof(ProductRemovedFromCart), typeof(CartEmptied), typeof(CartCheckedOut));

            this.RegisterHandlerFactoryWithTypes(
                () => new OrderEventHandler(new Repository(eventStore), dispatcher),
                typeof(OrderCreated), typeof(PaymentReceived), typeof(ShippingAddressConfirmed));
        }

        private void RegisterHandlerFactoryWithTypes(Func<IHandler> handler, params Type[] types)
        {
            foreach (var type in types)
            {
                this.handlerFactories.Add(type, new List<Func<IHandler>> { handler });
            }
        }

        public IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var evtType = evt.GetType();
            if (this.handlerFactories.ContainsKey(evtType))
            {
                var factories = this.handlerFactories[evtType];
                return factories.Select(h => (IEventHandler<TEvent>)h());
            }
            return new List<IEventHandler<TEvent>>();
        }
    }
}