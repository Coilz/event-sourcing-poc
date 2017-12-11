using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shop
{
    public class EmptyCart : ICommand
    {
        public EmptyCart(Guid cartId)
        {
            CartId = cartId;

        }
        public Guid CartId { get; }
    }
}
