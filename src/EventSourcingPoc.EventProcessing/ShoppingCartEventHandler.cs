using System.Linq;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages.Store;
using EventSourcingPoc.Readmodels;

namespace EventSourcingPoc.EventProcessing
{
    public class ShoppingCartEventHandler
        : IEventHandler<CartCreated>
        , IEventHandler<ProductAddedToCart>
        , IEventHandler<ProductRemovedFromCart>
        , IEventHandler<CartEmptied>
        , IEventHandler<CartCheckedOut>
    {
        private readonly IShoppingCartReadModelRepository _readModelStore;

        public ShoppingCartEventHandler(IShoppingCartReadModelRepository readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public void Handle(CartCreated evt)
        {
            var newCart = new ShoppingCartReadModel
            {
                CustomerId = evt.CustomerId,
                Id = evt.CartId
            };
            _readModelStore.SaveCart(newCart);
        }

        public void Handle(ProductAddedToCart evt)
        {
            var cart = _readModelStore.GetCartById(evt.CartId);
            var product = cart.Items.FirstOrDefault(x => x.ProductId == evt.ProductId);
            if (product == null)
                cart.Items.Add(new ShoppingCartItemReadModel
                {
                    Price = evt.Price,
                    ProductId = evt.ProductId
                });
            else
                product.Price = evt.Price;

            _readModelStore.SaveCart(cart);
        }

        public void Handle(ProductRemovedFromCart evt)
        {
            var cart = _readModelStore.GetCartById(evt.CartId);
            cart.Items.RemoveAll(x => x.ProductId == evt.ProductId);
            _readModelStore.SaveCart(cart);
        }

        public void Handle(CartEmptied evt)
        {
            var cart = _readModelStore.GetCartById(evt.CartId);
            cart.Items.Clear();
            _readModelStore.SaveCart(cart);
        }

        public void Handle(CartCheckedOut evt)
        {
            _readModelStore.RemoveCart(evt.CartId);
        }
    }
}
