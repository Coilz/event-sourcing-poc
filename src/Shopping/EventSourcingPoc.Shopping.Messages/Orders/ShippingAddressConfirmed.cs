using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class ShippingAddressConfirmed : IEvent
    {
        public Guid AggregateId { get; }
        public Address Address { get; }
        public ShippingAddressConfirmed(Guid aggregateId, Address address)
        {
            Address = address;
            AggregateId = aggregateId;
        }
    }
}
