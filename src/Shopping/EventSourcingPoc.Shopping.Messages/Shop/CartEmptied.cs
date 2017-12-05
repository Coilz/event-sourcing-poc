using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
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