using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class OrderCompleted : IEvent
    {
        public Guid AggregateId { get; }
        public OrderCompleted(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
