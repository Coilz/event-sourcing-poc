using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
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