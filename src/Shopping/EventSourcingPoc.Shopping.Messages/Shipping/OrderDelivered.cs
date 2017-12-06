using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class OrderDelivered : IEvent
    {
        public Guid AggregateId { get; }
        public OrderDelivered(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
