using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class ShipmentDelivered : Event
    {
        public ShipmentDelivered(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
