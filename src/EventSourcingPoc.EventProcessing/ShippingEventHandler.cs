using System;
using EventSourcingPoc.Domain.Shipping;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Shipping;

namespace EventSourcingPoc.EventProcessing
{
    public class ShippingEventHandler
        : EventHandler<ShippingSaga>
        , IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressConfirmed>
        , IEventHandler<PaymentConfirmed>
        , IEventHandler<AddressConfirmed>
        , IEventHandler<OrderDelivered>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ShippingEventHandler(IRepository repository, ICommandDispatcher commandDispatcher)
            : base(repository)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void Handle(OrderCreated @event)
        {
            var shippingSaga = ShippingSaga.Create(@event.OrderId);
            Repository.Save(shippingSaga);
        }

        public void Handle(PaymentReceived @event)
        {
            Execute(@event.OrderId, shippingSaga => shippingSaga.ConfirmPayment());
        }

        public void Handle(ShippingAddressConfirmed @event)
        {
            Execute(@event.OrderId, shippingSaga => shippingSaga.ConfirmAddress());
        }

        public void Handle(PaymentConfirmed @event)
        {
            Execute(@event.OrderId, shippingSaga => shippingSaga.CompleteIfPossible());
        }

        public void Handle(AddressConfirmed @event)
        {
            Execute(@event.OrderId, shippingSaga => shippingSaga.CompleteIfPossible());
        }

        public void Handle(OrderDelivered @event)
        {
            _commandDispatcher.Send(new CompleteOrder(@event.OrderId)); // TODO: This belongs in the saga
        }
    }
}
