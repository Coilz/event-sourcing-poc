using System;
using System.Collections.Generic;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class ShipmentCreated : Event
    {
        public ShipmentCreated(Guid aggregateId, Guid customerId, IEnumerable<ShipmentItem> shippingItems)
            : base(aggregateId)
        {
            CustomerId = customerId;
            ShippingItems = shippingItems;
        }

        public Guid CustomerId { get; }
        public IEnumerable<ShipmentItem> ShippingItems { get; }
    }
}
