using System;

namespace EventSourcingPoc.Messages.Shipping
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
