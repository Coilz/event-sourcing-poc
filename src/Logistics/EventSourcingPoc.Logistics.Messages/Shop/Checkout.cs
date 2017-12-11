using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shop
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