using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class ShippingProcessStarted : IEvent
    {
        public ShippingProcessStarted(Guid aggregateId)
            : base(aggregateId, 0)
        {
        }
    }
}
