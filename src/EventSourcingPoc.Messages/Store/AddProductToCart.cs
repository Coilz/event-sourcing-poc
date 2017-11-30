using System;

namespace EventSourcingPoc.Messages.Store
{
    public class AddProductToCart : ICommand
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public decimal Price { get; }
        public AddProductToCart(Guid cartId, Guid productId, decimal price)
        {
            this.ProductId = productId;
            this.CartId = cartId;
            this.Price = price;

        }
    }
}