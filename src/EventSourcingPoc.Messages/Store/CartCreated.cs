using System;

namespace EventSourcingPoc.Messages.Store
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
