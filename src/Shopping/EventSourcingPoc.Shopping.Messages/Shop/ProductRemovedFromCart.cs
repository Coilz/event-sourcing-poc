using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class ProductRemovedFromCart : IEvent
    {
        public ProductRemovedFromCart(Guid cartId, Guid productId)
        {
            CartId = cartId;
            ProductId = productId;

        }
        public Guid CartId { get; }
        public Guid ProductId { get; }
    }
}