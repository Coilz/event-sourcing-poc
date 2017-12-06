using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shop
{
    public class ProductAddedToCart : Event
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
