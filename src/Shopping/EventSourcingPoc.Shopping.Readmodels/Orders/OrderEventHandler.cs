using EventSourcingPoc.EventSourcing.Handlers;
using System;
using System.Linq;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shipping;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Orders
{
    public class OrderEventHandler
        : IEventHandler<OrderCreated>
        , IEventHandler<PaymentReceived>
        , IEventHandler<ShippingAddressProvided>
        , IEventHandler<OrderShipped>
        , IEventHandler<OrderDelivered>
        , IEventHandler<OrderCompleted>
    {
        private readonly IOrderReadModelRepository _repository;

        public OrderEventHandler(IOrderReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            var oderItems = @event.Items.Select(item => new OrderItemReadModel(item.ProductId, item.Price, item.Quantity));
            var model = new OrderReadModel(@event.AggregateId, @event.CustomerId, oderItems);
            await _repository.SaveAsync(model);
        }

        public async Task HandleAsync(PaymentReceived @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.Pay());
        }

        public async Task HandleAsync(ShippingAddressProvided @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.ProvideShippingAddress());
        }

        public async Task HandleAsync(OrderCompleted @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.Complete());
        }

        public async Task HandleAsync(OrderShipped @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.Ship());
        }

        public async Task HandleAsync(OrderDelivered @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.Deliver());
        }

        private async Task ExecuteSaveAsync(Guid id, Action<OrderReadModel> action)
        {
            var model = await _repository.GetAsync(id);
            action(model);
            await _repository.SaveAsync(model);
        }
    }
}
