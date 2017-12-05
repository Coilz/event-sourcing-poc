using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
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
