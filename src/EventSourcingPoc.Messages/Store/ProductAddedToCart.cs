using System;

namespace EventSourcingPoc.Messages.Store
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