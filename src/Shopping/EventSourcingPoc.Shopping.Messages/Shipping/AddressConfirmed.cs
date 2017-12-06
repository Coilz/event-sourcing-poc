using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class AddressConfirmed : IEvent
    {
        public Guid AggregateId { get; }
        public AddressConfirmed(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
