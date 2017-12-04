using System;

namespace EventSourcingPoc.Messages.Store
{
    public class RemoveProductFromCart : ICommand
    {
        public RemoveProductFromCart(Guid cartId, Guid productId)
        {
            CartId = cartId;
            ProductId = productId;

        }
        public Guid CartId { get; }
        public Guid ProductId { get; }

    }
}