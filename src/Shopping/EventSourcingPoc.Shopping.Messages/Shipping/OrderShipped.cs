using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class OrderShipped : Event
    {
        public OrderShipped(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
