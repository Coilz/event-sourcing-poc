using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public OrderItem[] Items { get; }

        public OrderCreated(Guid orderId, Guid customerId, params OrderItem[] items)
        {
            Items = items;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
