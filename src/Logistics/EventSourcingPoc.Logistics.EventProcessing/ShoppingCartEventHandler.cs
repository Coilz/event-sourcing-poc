using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.Domain.Orders;
using EventSourcingPoc.Logistics.Domain.Shop;
using EventSourcingPoc.Logistics.Messages.Orders;
using EventSourcingPoc.Logistics.Messages.Shop;

namespace EventSourcingPoc.Logistics.EventProcessing
{
    public class LogisticsCartEventHandler
        : IEventHandler<CartCheckedOut>
    {
        private readonly IRepository _repository;

        public LogisticsCartEventHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CartCheckedOut @event)
        {
            var cart = await _repository.GetByIdAsync<LogisticsCart>(@event.AggregateId);

            var orderItems = cart.LogisticsCartItems
                .Select(item =>
                    new OrderItem(item.ProductId, item.Price, item.Quantity));

            var order = Order.Create(@event.AggregateId, cart.CustomerId, orderItems); // TODO: Does this belong in a saga/process manager?
            await _repository.SaveAsync(order);
        }
    }
}
