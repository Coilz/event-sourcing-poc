using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class OrderCompleted : IEvent
    {
        public Guid OrderId { get; }
        public OrderCompleted(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}