using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class CartCreated : IEvent
    {
        public Guid CustomerId { get; }
        public CartCreated(Guid aggregateId, Guid customerId)
            : base(aggregateId, 0)
        {
            CustomerId = customerId;
        }
    }
}
