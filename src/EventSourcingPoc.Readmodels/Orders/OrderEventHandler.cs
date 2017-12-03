using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages.Orders;
using System;
using System.Linq;
using EventSourcingPoc.Messages.Shipping;

namespace EventSourcingPoc.Readmodels.Orders
{
    public class OrderEventHandler
        : IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressConfirmed>
        , IEventHandler<ShippingProcessStarted>
        , IEventHandler<OrderDelivered>
        , IEventHandler<OrderCompleted>
    {
        private readonly IOrderReadModelRepository _repository;

        public OrderEventHandler(IOrderReadModelRepository repository)
        {
            _repository = repository;
        }

        public void Handle(OrderCreated @event)
        {
            var oderItems = @event.Items.Select(item => new OrderItemReadModel(item.ProductId, item.Price, item.Quantity));
            var model = new OrderReadModel(@event.OrderId, @event.CustomerId, oderItems);
            _repository.Save(model);
        }

        public void Handle(PaymentReceived @event)
        {
            ExecuteSave(@event.OrderId, model => model.Pay());
        }

        public void Handle(ShippingAddressConfirmed @event)
        {
            ExecuteSave(@event.OrderId, model => model.ConfirmShippingAddress());
        }

        public void Handle(ShippingProcessStarted @event)
        {
            ExecuteSave(@event.OrderId, model => model.Ship());
        }

        public void Handle(OrderDelivered @event)
        {
            ExecuteSave(@event.OrderId, model => model.Deliver());
        }

        public void Handle(OrderCompleted @event)
        {
            ExecuteSave(@event.OrderId, model => model.Complete());
        }

        private void ExecuteSave(Guid id, Action<OrderReadModel> action)
        {
            var model = _repository.Get(id);
            action(model);
            _repository.Save(model);
        }
    }
}
