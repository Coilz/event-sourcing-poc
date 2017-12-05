using System;

namespace EventSourcingPoc.Messages.Shop
{
    public class CartCreated : IEvent
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public CartCreated(Guid cartId, Guid customerId)
        {
            CustomerId = customerId;
            CartId = cartId;
        }
    }
}
