using System;
using EventSourcingPoc.Logistics.Messages.Shipping;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shipment
{
    public class ShippingProcessStarted : Event
    {
        public ShippingProcessStarted(Guid aggregateId, Guid customerId, ShipmentItem[] items)
            : base(aggregateId)
        {
            CustomerId = customerId;
            Items = items;
        }

        public Guid CustomerId { get; }
        public ShipmentItem[] Items { get; }
    }
}
