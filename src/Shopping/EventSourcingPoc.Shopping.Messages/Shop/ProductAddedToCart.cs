using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class ProductAddedToCart : IEvent
    {
        public Guid AggregateId { get; }
        public Guid ProductId { get; }
        public decimal Price { get; }
        public ProductAddedToCart(Guid aggregateId, Guid productId, decimal price)
        {
            AggregateId = aggregateId;
            ProductId = productId;
            Price = price;
        }
    }
}
