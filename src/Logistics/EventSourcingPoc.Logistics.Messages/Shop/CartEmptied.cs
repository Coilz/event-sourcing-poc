using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shop
{
    public class CartEmptied : Event
    {
        public CartEmptied(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}