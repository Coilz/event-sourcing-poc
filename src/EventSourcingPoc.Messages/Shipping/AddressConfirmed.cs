using System;

namespace EventSourcingPoc.Messages.Shipping
{
    public class AddressConfirmed : IEvent
    {
        public Guid OrderId { get; }
        public AddressConfirmed(Guid orderId)
        {
            this.OrderId = orderId;

        }
    }
}
