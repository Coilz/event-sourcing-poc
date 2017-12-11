using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class ShippingProcessStarted : Event
    {
        public ShippingProcessStarted(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
