using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class CartEmptied : IEvent
    {
        public CartEmptied(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}