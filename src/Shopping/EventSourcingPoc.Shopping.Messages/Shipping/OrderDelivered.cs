using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class OrderDelivered : IEvent
    {
        public OrderDelivered(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
