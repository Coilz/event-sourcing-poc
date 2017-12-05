using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
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
