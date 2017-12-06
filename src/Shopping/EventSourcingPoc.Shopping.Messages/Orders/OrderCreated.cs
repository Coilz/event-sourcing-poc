using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class OrderCreated : IEvent
    {
        public Guid CustomerId { get; }
        public OrderItem[] Items { get; }

        public OrderCreated(Guid aggregateId, Guid customerId, params OrderItem[] items)
            : base(aggregateId, 0)
        {
            Items = items;
            CustomerId = customerId;
        }
    }
}
