using System;

namespace EventSourcingPoc.Domain.Store
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem(Guid productId, decimal price, int quantity)
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
