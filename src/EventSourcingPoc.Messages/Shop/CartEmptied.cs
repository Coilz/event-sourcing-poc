using System;

namespace EventSourcingPoc.Messages.Shop
{
    public class CartEmptied : IEvent
    {
        public CartEmptied(Guid cartId)
        {
            CartId = cartId;

        }
        public Guid CartId { get; }
    }
}