using System;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class ShippingItem
    {
        public Guid ItemId { get; }
        public ShippingItem(Guid itemId)
        {
            this.ItemId = itemId;

        }
    }
}
