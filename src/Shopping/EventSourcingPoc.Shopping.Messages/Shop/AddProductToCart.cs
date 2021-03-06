using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class AddProductToCart : ICommand
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public decimal Price { get; }
        public AddProductToCart(Guid cartId, Guid productId, decimal price)
        {
            ProductId = productId;
            CartId = cartId;
            Price = price;
        }
    }
}
