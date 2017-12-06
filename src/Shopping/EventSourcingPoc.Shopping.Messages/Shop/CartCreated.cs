using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class CartCreated : IEvent
    {
        public Guid AggregateId { get; }
        public Guid CustomerId { get; }
        public CartCreated(Guid aggregateId, Guid customerId)
        {
            CustomerId = customerId;
            AggregateId = aggregateId;
        }
    }
}
