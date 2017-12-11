using System;

namespace EventSourcingPoc.Readmodels.Shipping
{
    public class ShipmentItemReadModel
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ShipmentItemReadModel(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
