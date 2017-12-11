using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Logistics.Messages.Shop
{
    public class ProductRemovedFromCart : Event
    {
        public ProductRemovedFromCart(Guid aggregateId, int version, Guid productId)
            : base(aggregateId, version)
        {
            ProductId = productId;
        }
        public Guid ProductId { get; }
    }
}