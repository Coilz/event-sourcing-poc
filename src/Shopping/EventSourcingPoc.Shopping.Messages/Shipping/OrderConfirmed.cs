using System;
using System.Collections.Generic;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Messages.Orders;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class OrderConfirmed : Event
    {
        public OrderConfirmed(Guid aggregateId, int version, Guid customerId, IEnumerable<OrderItem> items)
            : base(aggregateId, version)
        {
            CustomerId = customerId;
            Items = items;
        }

        public Guid CustomerId { get; }
        public IEnumerable<OrderItem> Items { get; }
    }
}
