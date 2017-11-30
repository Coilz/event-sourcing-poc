using System;

namespace EventSourcingPoc.Messages.Shipping
{
    public class PaymentConfirmed : IEvent
    {
        public Guid OrderId { get; }
        public PaymentConfirmed(Guid orderId)
        {
            this.OrderId = orderId;

        }
    }
}
