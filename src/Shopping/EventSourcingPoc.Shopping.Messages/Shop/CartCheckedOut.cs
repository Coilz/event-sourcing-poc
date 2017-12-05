using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
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
