using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Logistics.Messages.Shop;

namespace EventSourcingPoc.Readmodels.Shop
{
    public class LogisticsCartEventHandler
        : IEventHandler<CartCreated>
        , IEventHandler<ProductAddedToCart>
        , IEventHandler<ProductRemovedFromCart>
        , IEventHandler<CartEmptied>
        , IEventHandler<CartCheckedOut>
    {
        private readonly ILogisticsCartReadModelRepository _repository;

        public LogisticsCartEventHandler(ILogisticsCartReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CartCreated @event)
        {
            var newCart = new LogisticsCartReadModel(@event.AggregateId, @event.CustomerId);
            await _repository.SaveAsync(newCart);
        }

        public async Task HandleAsync(ProductAddedToCart @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, cart =>
            {
                var cartItems = cart.Items.ToList();
                var cartItem = new LogisticsCartItemReadModel(@event.ProductId, @event.Price);

                cartItems.Add(cartItem);

                return new LogisticsCartReadModel(cart, cartItems);
            });
        }

        public async Task HandleAsync(ProductRemovedFromCart @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, cart =>
            {
                var productItems = cart.Items.Where(item => item.ProductId == @event.ProductId);
                var cartItems = cart.Items.Concat(productItems.Skip(1));

                return new LogisticsCartReadModel(cart, cartItems);
            });
        }

        public async Task HandleAsync(CartEmptied @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, cart => new LogisticsCartReadModel(cart));
        }

        public async Task HandleAsync(CartCheckedOut @event)
        {
            await _repository.RemoveAsync(@event.AggregateId);
        }

        private async Task ExecuteSaveAsync(Guid id, Func<LogisticsCartReadModel, LogisticsCartReadModel> transformation)
        {
            var model = await _repository.GetAsync(id);
            var updatedModel = transformation(model);
            await _repository.SaveAsync(updatedModel);
        }
    }
}
