using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class ProductAddedToCart : IEvent
    {
        public Guid ProductId { get; }
        public decimal Price { get; }
        public ProductAddedToCart(Guid aggregateId, int version, Guid productId, decimal price)
            : base(aggregateId, version)
        {
            ProductId = productId;
            Price = price;
        }
    }
}
