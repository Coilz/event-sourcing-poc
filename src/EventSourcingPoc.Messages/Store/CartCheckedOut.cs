using System;

namespace EventSourcingPoc.Messages.Store
{
    public class CartCheckedOut : IEvent
    {
        public CartCheckedOut(Guid cartId)
        {
            CartId = cartId;

        }
        public Guid CartId { get; }
    }
}
