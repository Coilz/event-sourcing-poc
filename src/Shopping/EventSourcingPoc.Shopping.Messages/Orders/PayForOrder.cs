using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
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
