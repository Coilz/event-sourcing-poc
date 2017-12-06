using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.Domain.Shipping;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shipping;

namespace EventSourcingPoc.Shopping.EventProcessing
{
    public class ShippingEventHandler
        : EventHandler<ShippingSaga>
        , IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressConfirmed>
        , IEventHandler<PaymentConfirmed>
        , IEventHandler<AddressConfirmed>
    {
        public ShippingEventHandler(IRepository repository)
            : base((IRepository) repository)
        {
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            var shippingSaga = ShippingSaga.Create(@event.AggregateId);
            await Repository.SaveAsync(shippingSaga);
        }

        public async Task HandleAsync(PaymentReceived @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.ConfirmPayment());
        }

        public async Task HandleAsync(ShippingAddressConfirmed @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.ConfirmAddress());
        }

        public async Task HandleAsync(PaymentConfirmed @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.CompleteIfPossible());
        }

        public async Task HandleAsync(AddressConfirmed @event)
        {
            await ExecuteAsync(@event.AggregateId, shippingSaga => shippingSaga.CompleteIfPossible());
        }
    }
}
