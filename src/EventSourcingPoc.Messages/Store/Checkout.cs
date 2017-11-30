using System;

namespace EventSourcingPoc.Messages.Store
{
    public class Checkout : ICommand
    {
        public Checkout(Guid cartId)
        {
            this.CartId = cartId;

        }
        public Guid CartId { get; }
    }
}