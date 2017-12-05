using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class ShippingAddressConfirmed : IEvent
    {
        public Guid OrderId { get; }
        public Address Address { get; }
        public ShippingAddressConfirmed(Guid orderId, Address address)
        {
            Address = address;
            OrderId = orderId;

        }
    }
}
