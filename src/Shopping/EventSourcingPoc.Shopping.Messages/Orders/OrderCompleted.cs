using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class OrderCompleted : IEvent
    {
        public OrderCompleted(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
