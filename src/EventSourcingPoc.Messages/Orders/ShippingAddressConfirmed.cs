using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class ShippingAddressConfirmed : IEvent
    {
        public Guid OrderId { get; }
        public Address Address { get; }
        public ShippingAddressConfirmed(Guid orderId, Address address)
        {
            this.Address = address;
            this.OrderId = orderId;

        }
    }
}
