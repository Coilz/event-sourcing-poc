using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class ShipmentStarted : Event
    {
        public ShipmentStarted(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
