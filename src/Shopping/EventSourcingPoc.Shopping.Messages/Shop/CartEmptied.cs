using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class CartEmptied : IEvent
    {
        public CartEmptied(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
        public Guid AggregateId { get; }
    }
}