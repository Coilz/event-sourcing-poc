using System;
using System.Linq;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages.Shop;

namespace EventSourcingPoc.Readmodels.Store
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

        public void Handle(CartCreated @event)
        {
            var newCart = new ShoppingCartReadModel(@event.CartId, @event.CustomerId);
            _repository.Save(newCart);
        }

        public void Handle(ProductAddedToCart @event)
        {
            ExecuteSave(@event.CartId, cart =>
            {
                var cartItems = cart.Items.ToList();
                var cartItem = new ShoppingCartItemReadModel(@event.ProductId, @event.Price);

                cartItems.Add(cartItem);

                return new ShoppingCartReadModel(cart, cartItems);
            });
        }

        public void Handle(ProductRemovedFromCart @event)
        {
            ExecuteSave(@event.CartId, cart =>
            {
                var productItems = cart.Items.Where(item => item.ProductId == @event.ProductId);
                var cartItems = cart.Items.Concat(productItems.Skip(1));

                return new ShoppingCartReadModel(cart, cartItems);
            });
        }

        public void Handle(CartEmptied @event)
        {
            ExecuteSave(@event.CartId, cart => new ShoppingCartReadModel(cart));
        }

        public void Handle(CartCheckedOut @event)
        {
            _repository.Remove(@event.CartId);
        }

        private void ExecuteSave(Guid id, Func<ShoppingCartReadModel, ShoppingCartReadModel> transformation)
        {
            var model = _repository.Get(id);
            var updatedModel = transformation(model);
            _repository.Save(updatedModel);
        }
    }
}
