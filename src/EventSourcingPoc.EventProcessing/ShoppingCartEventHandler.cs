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
            var cart = _readModelRepository.GetCartById(evt.CartId);
            var product = cart.Items.FirstOrDefault(x => x.ProductId == evt.ProductId);
            if (product == null)
            {
                var cartItems = cart.Items.ToList();
                cartItems.Add(new ShoppingCartItemReadModel
                {
                    Price = evt.Price,
                    ProductId = evt.ProductId
                });
                cart = new ShoppingCartReadModel(cart, cartItems);
            }
            else
                product.Price = evt.Price;

            _readModelRepository.SaveCart(cart);
        }

        public void Handle(ProductRemovedFromCart evt)
        {
            var cart = _readModelRepository.GetCartById(evt.CartId);
            var cartItems = cart.Items.Where(item => item.ProductId != evt.ProductId);
            cart = new ShoppingCartReadModel(cart, cartItems);
            _readModelRepository.SaveCart(cart);
        }

        public void Handle(CartEmptied evt)
        {
            var cart = _readModelRepository.GetCartById(evt.CartId);
            cart = new ShoppingCartReadModel(cart);
            _readModelRepository.SaveCart(cart);
        }

        public void Handle(CartCheckedOut evt)
        {
            _readModelRepository.RemoveCart(evt.CartId);
        }
    }
}
