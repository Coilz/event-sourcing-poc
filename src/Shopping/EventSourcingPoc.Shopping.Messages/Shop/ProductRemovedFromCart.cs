using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class ProductRemovedFromCart : IEvent
    {
        public ProductRemovedFromCart(Guid aggregateId, Guid productId)
        {
            AggregateId = aggregateId;
            ProductId = productId;
        }
        public Guid AggregateId { get; }
        public Guid ProductId { get; }
    }
}