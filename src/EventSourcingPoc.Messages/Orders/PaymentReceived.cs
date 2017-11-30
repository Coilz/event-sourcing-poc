using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class PaymentReceived : IEvent
    {
        public Guid OrderId { get; }
        public PaymentReceived(Guid orderId)
        {
            this.OrderId = orderId;

        }
    }
}
