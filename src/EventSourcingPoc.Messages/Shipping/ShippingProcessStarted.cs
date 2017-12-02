using System;

namespace EventSourcingPoc.Messages.Shipping
{
    public class ShippingProcessStarted : IEvent
    {
        public Guid OrderId { get; }
        public ShippingProcessStarted(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
