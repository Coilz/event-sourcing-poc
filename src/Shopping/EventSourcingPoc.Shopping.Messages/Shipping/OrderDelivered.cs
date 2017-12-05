using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class OrderDelivered : IEvent
    {
        public Guid OrderId { get; }
        public OrderDelivered(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
