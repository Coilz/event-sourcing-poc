using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class ProductAddedToCart : IEvent
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public decimal Price { get; }
        public ProductAddedToCart(Guid cartId, Guid productId, decimal price)
        {
            CartId = cartId;
            ProductId = productId;
            Price = price;
        }
    }
}