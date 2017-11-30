using System;

namespace EventSourcingPoc.Messages.Store
{
    public class ProductRemovedFromCart : IEvent
    {
        public ProductRemovedFromCart(Guid cartId, Guid productId)
        {
            this.CartId = cartId;
            this.ProductId = productId;

        }
        public Guid CartId { get; }
        public Guid ProductId { get; }
    }
}