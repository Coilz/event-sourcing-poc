using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Orders
{
    public class OrderCompleted : Event
    {
        public OrderCompleted(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
