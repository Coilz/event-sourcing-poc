using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
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
