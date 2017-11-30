using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class OrderItem
    {
        public Guid ProductId { get; }
        public decimal Price { get; }

        public OrderItem(Guid productId, decimal price)
        {
            this.Price = price;
            this.ProductId = productId;
        }
    }
}
