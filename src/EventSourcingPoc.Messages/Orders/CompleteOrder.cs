using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class CompleteOrder : ICommand
    {
        public Guid OrderId { get; }
        public CompleteOrder(Guid orderId)
        {
            this.OrderId = orderId;

        }
    }
}
