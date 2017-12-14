using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.Domain.Shipping;
using EventSourcingPoc.Shopping.Domain.Shop;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shipping;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Shopping.EventProcessing
{
    public class ShippingEventHandler
        : EventSourcingPoc.EventProcessing.EventHandler<ShippingSaga>
        , IEventHandler<CartCheckedOut>
        , IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressProvided>
        , IEventHandler<PaymentConfirmed>
        , IEventHandler<AddressConfirmed>
    {
        public ShippingEventHandler(IRepository repository)
            : base(repository)
        {
        }

        public async Task HandleAsync(CartCheckedOut @event)
        {
            var cart = await Repository.GetByIdAsync<ShoppingCart>(@event.AggregateId);

            var shippingSaga = ShippingSaga.Create(Guid.NewGuid(), @event.AggregateId, cart.CustomerId, cart.ShoppingCartItems);
            await Repository.SaveAsync(shippingSaga);
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.ConfirmOrder(@event.CustomerId, @event.Items));
        }

        public async Task HandleAsync(PaymentReceived @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.ConfirmPayment());
        }

        public async Task HandleAsync(ShippingAddressProvided @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.ConfirmAddress());
        }

        public async Task HandleAsync(PaymentConfirmed @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.CompleteForShipping());
        }

        public async Task HandleAsync(AddressConfirmed @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.CompleteForShipping());
        }
    }
}
