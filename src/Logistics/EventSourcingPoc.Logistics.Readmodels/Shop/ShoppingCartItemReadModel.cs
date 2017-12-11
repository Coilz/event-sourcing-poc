using System;

namespace EventSourcingPoc.Readmodels.Shop
{
    public class LogisticsCartItemReadModel
    {
        public LogisticsCartItemReadModel(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
        public Guid ProductId { get; }
        public decimal Price { get; }
    }
}
