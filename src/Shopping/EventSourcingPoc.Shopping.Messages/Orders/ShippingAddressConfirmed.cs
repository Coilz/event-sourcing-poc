using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class ShippingAddressConfirmed : IEvent
    {
        public Address Address { get; }
        public ShippingAddressConfirmed(Guid aggregateId, int version, Address address)
            : base(aggregateId, version)
        {
            Address = address;
        }
    }
}
