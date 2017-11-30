using System;

namespace EventSourcingPoc.Messages.Shipping
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
