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
        , IEventHandler<StartedShippingProcess>
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
            ExecuteSave(@event.OrderId, model =>
            {
                model.Pay();
                return model;
            });
        }

        public void Handle(ShippingAddressConfirmed @event)
        {
            ExecuteSave(@event.OrderId, model =>
            {
                model.ConfirmShippingAddress();
                return model;
            });
        }

        public void Handle(StartedShippingProcess @event)
        {
            ExecuteSave(@event.OrderId, model =>
            {
                model.Ship();
                return model;
            });
        }

        public void Handle(OrderDelivered @event)
        {
            ExecuteSave(@event.OrderId, model =>
            {
                model.Deliver();
                return model;
            });
        }

        public void Handle(OrderCompleted @event)
        {
            ExecuteSave(@event.OrderId, model =>
            {
                model.Complete();
                return model;
            });
        }

        private void ExecuteSave(Guid id, Func<OrderReadModel, OrderReadModel> transformation)
        {
            var model = _repository.Get(id);
            var updatedModel = transformation(model);
            _repository.Save(updatedModel);
        }
    }
}
