using System;

namespace EventSourcingPoc.Messages.Shop
{
    public class Checkout : ICommand
    {
        public Checkout(Guid cartId)
        {
            CartId = cartId;

        }
        public Guid CartId { get; }
    }
}