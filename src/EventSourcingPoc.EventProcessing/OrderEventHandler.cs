using EventSourcingPoc.Domain.Shipping;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;

namespace EventSourcingPoc.EventProcessing
{
    public class OrderEventHandler
        : IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressConfirmed>
    {
        private readonly IRepository _repository;
        private readonly ICommandDispatcher _dispatcher;

        public OrderEventHandler(IRepository repository, ICommandDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }

        public void Handle(OrderCreated evt)
        {
            var saga = ShippingSaga.Create(evt.OrderId);
            _repository.Save(saga);
        }

        public void Handle(PaymentReceived evt)
        {
            var saga = _repository.GetById<ShippingSaga>(evt.OrderId);
            saga.ConfirmPayment(_dispatcher);
            _repository.Save(saga);
        }

        public void Handle(ShippingAddressConfirmed evt)
        {
            var saga = _repository.GetById<ShippingSaga>(evt.OrderId);
            saga.ConfirmAddress(_dispatcher);
            _repository.Save(saga);
        }
    }
}
