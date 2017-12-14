using System;
using System.Collections.Generic;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class PlaceOrder : ICommand
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public IEnumerable<OrderItem> OrderItems { get; }

        public PlaceOrder(Guid orderId, Guid customerId, IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderItems = orderItems;
        }
    }
}
