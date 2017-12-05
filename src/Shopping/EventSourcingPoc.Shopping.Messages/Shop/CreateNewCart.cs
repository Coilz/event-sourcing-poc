using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class CreateNewCart : ICommand
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public CreateNewCart(Guid cartId, Guid customerId)
        {
            CustomerId = customerId;
            CartId = cartId;
        }
    }
}
