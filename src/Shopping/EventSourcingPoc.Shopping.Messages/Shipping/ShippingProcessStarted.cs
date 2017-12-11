using System;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Messages.Orders;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class ShippingProcessStarted : Event
    {
        public ShippingProcessStarted(Guid aggregateId, Guid customerId, OrderItem[] items)
            : base(aggregateId)
        {
            CustomerId = customerId;
            Items = items;
        }

        public Guid CustomerId { get; }
        public OrderItem[] Items { get; }
    }
}
