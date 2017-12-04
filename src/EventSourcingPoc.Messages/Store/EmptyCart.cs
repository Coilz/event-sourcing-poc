using System;

namespace EventSourcingPoc.Messages.Store
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
