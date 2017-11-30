using System;

namespace EventSourcingPoc.Messages.Store
{
    public class CartEmptied : IEvent
    {
        public CartEmptied(Guid cartId)
        {
            this.CartId = cartId;

        }
        public Guid CartId { get; }
    }
}