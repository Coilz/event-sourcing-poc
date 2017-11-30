using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public OrderItem[] Items { get; }

        public OrderCreated(Guid orderId, Guid clientId, params OrderItem[] items)
        {
            this.Items = items;
            this.ClientId = clientId;
            this.OrderId = orderId;

        }
    }
}
