using System;

namespace EventSourcingPoc.Logistics.Domain.Shop
{
    public class LogisticsCartItem
    {
        public LogisticsCartItem(Guid productId, decimal price, int quantity)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public void AddItems(int quantity)
        {
            Quantity += quantity;
        }

        public void RemoveItems(int quantity)
        {
            if (quantity > Quantity)
                throw new InvalidOperationException($"A shopping cart item cannot have a negative quantity.");

            Quantity -= quantity;
        }

        public Guid ProductId { get; }
        public decimal Price { get; }
        public int Quantity { get; private set; }
    }
}
