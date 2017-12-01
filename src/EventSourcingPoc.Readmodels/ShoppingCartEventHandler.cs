using System;
using System.Linq;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages.Store;
using EventSourcingPoc.Readmodels;

namespace EventSourcingPoc.Readmodels
{
    public class ShoppingCartEventHandler
        : IEventHandler<CartCreated>
        , IEventHandler<ProductAddedToCart>
        , IEventHandler<ProductRemovedFromCart>
        , IEventHandler<CartEmptied>
        , IEventHandler<CartCheckedOut>
    {
        private readonly IShoppingCartReadModelRepository _readModelRepository;

        public ShoppingCartEventHandler(IShoppingCartReadModelRepository readModelRepository)
        {
            _readModelRepository = readModelRepository;
        }

        public void Handle(CartCreated evt)
        {
            var newCart = new ShoppingCartReadModel(evt.CartId, evt.CustomerId);
            _readModelRepository.SaveCart(newCart);
        }

        public void Handle(ProductAddedToCart evt)
        {
            ExecuteSave(evt.CartId, cart =>
            {
                var cartItems = cart.Items
                    .Where(x => x.ProductId != evt.ProductId)
                    .ToList();
                var cartItem = new ShoppingCartItemReadModel(evt.ProductId, evt.Price);

                cartItems.Add(cartItem);

                return new ShoppingCartReadModel(cart, cartItems);
            });
        }

        public void Handle(ProductRemovedFromCart evt)
        {
            ExecuteSave(evt.CartId, cart =>
            {
                var cartItems = cart.Items.Where(item => item.ProductId != evt.ProductId);
                return new ShoppingCartReadModel(cart, cartItems);
            });
        }

        public void Handle(CartEmptied evt)
        {
            ExecuteSave(evt.CartId, cart => new ShoppingCartReadModel(cart));
        }

        public void Handle(CartCheckedOut evt)
        {
            _readModelRepository.RemoveCart(evt.CartId);
        }

        private void ExecuteSave(Guid id, Func<ShoppingCartReadModel, ShoppingCartReadModel> transformation)
        {
            var cart = _readModelRepository.GetCartById(id);
            var updatedCart = transformation(cart);
            _readModelRepository.SaveCart(updatedCart);
        }
    }
}
