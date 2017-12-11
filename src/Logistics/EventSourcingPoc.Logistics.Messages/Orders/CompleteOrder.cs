using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Orders
{
    public class CompleteOrder : ICommand
    {
        public Guid OrderId { get; }
        public CompleteOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
