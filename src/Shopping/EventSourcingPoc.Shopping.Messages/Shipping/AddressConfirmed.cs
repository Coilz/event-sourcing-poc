using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class AddressConfirmed : Event
    {
        public AddressConfirmed(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
