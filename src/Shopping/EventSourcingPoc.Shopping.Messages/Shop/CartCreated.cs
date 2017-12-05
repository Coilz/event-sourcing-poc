using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
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
