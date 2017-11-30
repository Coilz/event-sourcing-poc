using System;

namespace EventSourcingPoc.Messages.Store
{
    public class CreateNewCart : ICommand
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public CreateNewCart(Guid cartId, Guid customerId)
        {
            this.CustomerId = customerId;
            this.CartId = cartId;
        }
    }
}
