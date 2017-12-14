using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class ShippingAddressProvided : Event
    {
        public Address Address { get; }
        public ShippingAddressProvided(Guid aggregateId, int version, Address address)
            : base(aggregateId, version)
        {
            Address = address;
        }
    }
}
