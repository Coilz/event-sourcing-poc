using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class PayForOrder : ICommand
    {
        public Guid OrderId { get; }
        public PayForOrder(Guid orderId)
        {
            this.OrderId = orderId;

        }
    }
}
