using System;

namespace EventSourcingPoc.Logistics.Messages.Orders
{
    public class OrderItem
    {
        public Guid ProductId { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public OrderItem(Guid productId, decimal price, int quantity)
        {
            Price = price;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
