using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class CartCreated : Event
    {
        public Guid CustomerId { get; }
        public CartCreated(Guid aggregateId, Guid customerId)
            : base(aggregateId)
        {
            CustomerId = customerId;
        }
    }
}
