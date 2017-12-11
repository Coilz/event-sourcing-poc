using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shop
{
    public class CartCheckedOut : Event
    {
        public CartCheckedOut(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
