using EventSourcingPoc.Domain.Shipping;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Shipping;

namespace EventSourcingPoc.EventProcessing
{
    public class OrderEventHandler
        : IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressConfirmed>
        , IEventHandler<PaymentConfirmed>
        , IEventHandler<AddressConfirmed>
        , IEventHandler<OrderDelivered>
    {
        private readonly IRepository _repository;
        private readonly ICommandDispatcher _dispatcher;

        public OrderEventHandler(IRepository repository, ICommandDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }

        public void Handle(OrderCreated @event)
        {
            var saga = ShippingSaga.Create(@event.OrderId);
            _repository.Save(saga);
        }

        public void Handle(PaymentReceived @event)
        {
            var saga = _repository.GetById<ShippingSaga>(@event.OrderId);
            saga.ConfirmPayment();
            _repository.Save(saga);
        }

        public void Handle(ShippingAddressConfirmed @event)
        {
            var saga = _repository.GetById<ShippingSaga>(@event.OrderId);
            saga.ConfirmAddress();
            _repository.Save(saga);
        }

        public void Handle(PaymentConfirmed @event)
        {
            var saga = _repository.GetById<ShippingSaga>(@event.OrderId);
            saga.CompleteIfPossible();
            _repository.Save(saga);
        }

        public void Handle(AddressConfirmed @event)
        {
            var saga = _repository.GetById<ShippingSaga>(@event.OrderId);
            saga.CompleteIfPossible();
            _repository.Save(saga);
        }

        public void Handle(OrderDelivered @event)
        {
            _dispatcher.Send(new CompleteOrder(@event.OrderId));
        }
    }
}
