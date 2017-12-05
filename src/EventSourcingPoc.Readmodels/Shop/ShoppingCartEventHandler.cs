using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Readmodels.Shop
{
    public class ShoppingCartEventHandler
        : IEventHandler<CartCreated>
        , IEventHandler<ProductAddedToCart>
        , IEventHandler<ProductRemovedFromCart>
        , IEventHandler<CartEmptied>
        , IEventHandler<CartCheckedOut>
    {
        private readonly IShoppingCartReadModelRepository _repository;

        public ShoppingCartEventHandler(IShoppingCartReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CartCreated @event)
        {
            var newCart = new ShoppingCartReadModel(@event.CartId, @event.CustomerId);
            await _repository.SaveAsync(newCart);
        }

        public async Task HandleAsync(ProductAddedToCart @event)
        {
            await ExecuteSaveAsync(@event.CartId, cart =>
            {
                var cartItems = cart.Items.ToList();
                var cartItem = new ShoppingCartItemReadModel(@event.ProductId, @event.Price);

                cartItems.Add(cartItem);

                return new ShoppingCartReadModel(cart, cartItems);
            });
        }

        public async Task HandleAsync(ProductRemovedFromCart @event)
        {
            await ExecuteSaveAsync(@event.CartId, cart =>
            {
                var productItems = cart.Items.Where(item => item.ProductId == @event.ProductId);
                var cartItems = cart.Items.Concat(productItems.Skip(1));

                return new ShoppingCartReadModel(cart, cartItems);
            });
        }

        public async Task HandleAsync(CartEmptied @event)
        {
            await ExecuteSaveAsync(@event.CartId, cart => new ShoppingCartReadModel(cart));
        }

        public async Task HandleAsync(CartCheckedOut @event)
        {
            await _repository.RemoveAsync(@event.CartId);
        }

        private async Task ExecuteSaveAsync(Guid id, Func<ShoppingCartReadModel, ShoppingCartReadModel> transformation)
        {
            var model = await _repository.GetAsync(id);
            var updatedModel = transformation(model);
            await _repository.SaveAsync(updatedModel);
        }
    }
}
