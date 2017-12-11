using EventSourcingPoc.Logistics.Messages.Shipping;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingPoc.Logistics.Messages.Shipment
{
    public class CreateShipment : ICommand
    {
        public CreateShipment(Guid id, Guid customerId, IEnumerable<ShipmentItem> shippingItems)
        {
            Id = id;
            CustomerId = customerId;
            ShippingItems = shippingItems;
        }

        public Guid Id { get; }
        public Guid CustomerId { get; }
        public IEnumerable<ShipmentItem> ShippingItems { get; }
    }
}
