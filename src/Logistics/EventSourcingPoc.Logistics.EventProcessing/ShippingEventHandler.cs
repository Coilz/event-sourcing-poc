using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.Domain.Shipping;
using EventSourcingPoc.Logistics.Messages.Orders;
using EventSourcingPoc.Logistics.Messages.Shipping;

namespace EventSourcingPoc.Logistics.EventProcessing
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
