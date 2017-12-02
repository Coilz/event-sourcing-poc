using System;

namespace EventSourcingPoc.Messages.Shipping
{
    public class StartedShippingProcess : IEvent
    {
        public Guid OrderId { get; }
        public StartedShippingProcess(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
