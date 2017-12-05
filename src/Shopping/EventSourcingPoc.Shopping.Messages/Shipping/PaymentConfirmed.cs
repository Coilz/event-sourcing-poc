using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
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
