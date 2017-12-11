using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shipping
{
    public class AddressConfirmed : Event
    {
        public AddressConfirmed(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
