using System;

namespace EventSourcingPoc.Readmodels.Orders
{
    public class OrderItemReadModel
    {
        public Guid ProductId { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public OrderItemReadModel(Guid productId, decimal price, int quantity)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
    }
}
