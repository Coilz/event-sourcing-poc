using System;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class ShipmentItem
    {
        public ShipmentItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; }
        public int Quantity { get; }
    }
}
