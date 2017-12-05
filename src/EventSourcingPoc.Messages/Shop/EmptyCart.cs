using System;

namespace EventSourcingPoc.Messages.Shop
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
