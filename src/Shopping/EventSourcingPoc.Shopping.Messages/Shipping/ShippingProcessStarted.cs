using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class ShippingProcessStarted : IEvent
    {
        public Guid AggregateId { get; }
        public ShippingProcessStarted(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
