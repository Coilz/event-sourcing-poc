using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.Domain.Orders;
using EventSourcingPoc.Shopping.Domain.Shop;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Shopping.EventProcessing
{
    public class ShoppingCartEventHandler
        : IEventHandler<CartCheckedOut>
    {
        private readonly IRepository _repository;

        public ShoppingCartEventHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CartCheckedOut @event)
        {
            var cart = await _repository.GetByIdAsync<ShoppingCart>(@event.AggregateId);

            var orderItems = cart.ShoppingCartItems
                .Select(item =>
                    new OrderItem(item.ProductId, item.Price, item.Quantity));

            var order = Order.Create(@event.AggregateId, cart.CustomerId, orderItems); // TODO: Does this belong in a saga/process manager?
            await _repository.SaveAsync(order);
        }
    }
}
