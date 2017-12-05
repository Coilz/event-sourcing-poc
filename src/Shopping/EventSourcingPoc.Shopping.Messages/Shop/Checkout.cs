using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
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